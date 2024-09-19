using idpa_vorprojekt_gewinnverteilung.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace UnitTests
{
    [TestClass]
    public class RemarkManagerTests
    {
        private RemarkManager remarkManager;
        private StackPanel remarksPanel;
        private Border remarkDetailPanel;
        private TextBlock remarkDetailTitle;
        private TextBlock remarkDetailContent;
        private Thread uiThread;
        private AutoResetEvent uiInitialized = new AutoResetEvent(false);

        [TestInitialize]
        public void Setup()
        {
            uiThread = new Thread(() =>
            {
                remarksPanel = new StackPanel();
                remarkDetailPanel = new Border();
                remarkDetailTitle = new TextBlock();
                remarkDetailContent = new TextBlock();
                remarkManager = new RemarkManager(remarksPanel, remarkDetailPanel, remarkDetailTitle, remarkDetailContent);
                uiInitialized.Set();
                Dispatcher.Run();
            });
            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.Start();
            uiInitialized.WaitOne();
        }

        [TestCleanup]
        public void Cleanup()
        {
            remarksPanel.Dispatcher.InvokeShutdown();
            uiThread.Join();
        }

        [TestMethod]
        [STAThread]
        public void RemarkGeneration_ValidCalculation_CorrectRemarksGenerated()
        {
            // Arrange
            string title = "Calculation Completed";
            string content = "The calculation of legal retained earnings and dividend has been successfully completed.";

            // Act
            remarksPanel.Dispatcher.Invoke(() =>
            {
                remarkManager.AddRemark(title, content);
            });

            var remarks = new List<Remark>();
            remarksPanel.Dispatcher.Invoke(() =>
            {
                remarks = remarkManager.GetAllRemarks();
            });

            // Assert
            Assert.AreEqual(1, remarks.Count);
            Assert.AreEqual(title, remarks[0].Title);
            Assert.AreEqual(content, remarks[0].Content);
        }

        [TestMethod]
        [STAThread]
        public void AddRemark_NewRemarkAdded_SuccessfullyDisplayed()
        {
            // Arrange
            string title = "New Remark";
            string content = "This is a new remark.";

            // Act
            remarksPanel.Dispatcher.Invoke(() =>
            {
                remarkManager.AddRemark(title, content);
            });

            var remarks = new List<Remark>();
            remarksPanel.Dispatcher.Invoke(() =>
            {
                remarks = remarkManager.GetAllRemarks();
            });

            // Assert
            Assert.AreEqual(1, remarks.Count);
            Assert.AreEqual(title, remarks[0].Title);
            Assert.AreEqual(content, remarks[0].Content);
        }
    }
}
