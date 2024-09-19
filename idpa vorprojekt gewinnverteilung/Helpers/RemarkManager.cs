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

    public void AddRemark(string title, string content)
    {
        remarks.Add(new Remark { Title = title, Content = content });
        DisplayRemarks();
    }

    public void RemoveRemark(Remark remark)
    {
        remarks.Remove(remark);
        DisplayRemarks();
    }

    public List<Remark> GetAllRemarks()
    {
        return remarks;
    }

    public void DisplayRemarks()
    {
        remarksPanel.Children.Clear();
        foreach (var remark in remarks)
        {
            var remarkControl = new Border
            {
                AddRemark("No Remarks", "There are currently no remarks available.");
            }

            remarksPanel.Visibility = Visibility.Visible;
        }

    private void ShowRemarkDetail(Remark remark)
    {
        remarkDetailPanel.Visibility = Visibility.Visible;
        remarkDetailTitle.Text = remark.Title;
        remarkDetailContent.Text = remark.Content;

        var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
        remarkDetailPanel.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
    }

    public void CloseRemarkDetail()
    {
        var fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
        fadeOutAnimation.Completed += (s, a) =>
        {
        {
        }

        // Clears all remarks
        public void ClearRemarks()
        {
            remarksPanel.Children.Clear();
        }
    }
        }
    }
}
