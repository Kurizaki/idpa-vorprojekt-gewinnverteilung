using System.Windows;
using System.Windows.Controls;

namespace idpa_vorprojekt_gewinnverteilung.Helpers
{
    public class RemarkManager
    {
        private readonly StackPanel remarksPanel;
        private readonly StackPanel remarkDetailPanel;
        private readonly TextBlock remarkDetailTitle;
        private readonly TextBlock remarkDetailContent;
        private Border remarkDetailPanel1;

        public RemarkManager(StackPanel remarksPanel, StackPanel remarkDetailPanel, TextBlock remarkDetailTitle, TextBlock remarkDetailContent)
        {
            this.remarksPanel = remarksPanel;
            this.remarkDetailPanel = remarkDetailPanel;
            this.remarkDetailTitle = remarkDetailTitle;
            this.remarkDetailContent = remarkDetailContent;
        }

        public RemarkManager(StackPanel remarksPanel, Border remarkDetailPanel1, TextBlock remarkDetailTitle, TextBlock remarkDetailContent)
        {
            this.remarksPanel = remarksPanel;
            this.remarkDetailPanel1 = remarkDetailPanel1;
            this.remarkDetailTitle = remarkDetailTitle;
            this.remarkDetailContent = remarkDetailContent;
        }

        // Adds a new remark with title and description
        public void AddRemark(string title, string description)
        {
            // Create a new TextBlock for the remark
            TextBlock remark = new TextBlock
            {
                Text = title,
                Margin = new Thickness(5),
                FontWeight = FontWeights.Bold,
                Cursor = System.Windows.Input.Cursors.Hand
            };

            // Handle click event to show remark details
            remark.MouseLeftButtonDown += (s, e) => DisplayRemarkDetails(title, description);

            // Add the new remark to the remarks panel
            remarksPanel.Children.Add(remark);
        }

        // Displays the remarks on the main window
        public void DisplayRemarks()
        {
            if (remarksPanel.Children.Count == 0)
            {
                AddRemark("No Remarks", "There are currently no remarks available.");
            }

            remarksPanel.Visibility = Visibility.Visible;
        }

        // Displays the details of a remark in a detail panel
        private void DisplayRemarkDetails(string title, string description)
        {
            remarkDetailTitle.Text = title;
            remarkDetailContent.Text = description;
            remarkDetailPanel.Visibility = Visibility.Visible;
        }

        // Closes the remark detail view
        public void CloseRemarkDetail()
        {
            remarkDetailPanel.Visibility = Visibility.Collapsed;
        }

        // Clears all remarks
        public void ClearRemarks()
        {
            remarksPanel.Children.Clear();
        }
    }
}
