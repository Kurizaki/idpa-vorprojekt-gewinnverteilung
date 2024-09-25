using idpa_vorprojekt_gewinnverteilung;
using idpa_vorprojekt_gewinnverteilung.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace UnitTests
{
    [TestClass]
    public class MainWindowTests
    {
        private MainWindow mainWindow = null!;
        private InputValidation inputValidation = null!;
        private RemarkManager remarkManager = null!;
        private CalculationLogic calculationLogic = null!;
        private Thread uiThread = null!;
        private AutoResetEvent uiInitialized = new AutoResetEvent(false);

        [TestInitialize]
        public void Setup()
        {
            uiThread = new Thread(() =>
            {
                mainWindow = new MainWindow();
                remarkManager = new RemarkManager(new StackPanel(), new Border(), new TextBlock(), new TextBlock());
                inputValidation = new InputValidation(remarkManager);
                calculationLogic = new CalculationLogic();
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
            mainWindow.Dispatcher.InvokeShutdown();
            uiThread.Join();
        }

        [TestMethod]
        [STAThread]
        public void PositiveTest_ValidInputs_AcceptedAndCalculationStarted()
        {
            // Arrange
            string annualProfit = "100000";
            string shares = "500000";
            string reserves = "20000";
            string carryforward = "50000";
            string dividend = "5";

            // Act
            mainWindow.Dispatcher.Invoke(() =>
            {
                mainWindow.OnSubmitClick(annualProfit, shares, reserves, carryforward, dividend);
            });

            // Assert
            string retainedEarningsOutputText = string.Empty;
            string dividendOutputText = string.Empty;
            string carryforwardOutputText = string.Empty;

            mainWindow.Dispatcher.Invoke(() =>
            {
                retainedEarningsOutputText = mainWindow.GetRetainedEarningsOutput();
                dividendOutputText = mainWindow.GetDividendOutput();
                carryforwardOutputText = mainWindow.GetCarryforwardOutput();
            });

            Assert.AreEqual("25000.00 CHF", retainedEarningsOutputText);
            Assert.AreEqual("5.00 CHF", dividendOutputText);
            Assert.AreEqual("74995.00 CHF", carryforwardOutputText);
        }

        [TestMethod]
        [STAThread]
        public void PositiveTest_AddNewRemark_Displayed()
        {
            // Arrange
            string title = "New Remark";
            string content = "This is a new remark.";

            // Act
            mainWindow.Dispatcher.Invoke(() =>
            {
                remarkManager.AddRemark(title, content);
            });

            var remarks = new List<Remark>();
            mainWindow.Dispatcher.Invoke(() =>
            {
                remarks = remarkManager.GetAllRemarks();
            });

            Assert.AreEqual(1, remarks.Count);
            Assert.AreEqual(title, remarks[0].Title);
            Assert.AreEqual(content, remarks[0].Content);
        }
    }
}
