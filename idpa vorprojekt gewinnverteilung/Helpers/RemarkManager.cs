using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace idpa_vorprojekt_gewinnverteilung.Helpers
{
    public class Remark
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class RemarkManager
    {
        private StackPanel remarksPanel;
        private Border remarkDetailPanel;
        private TextBlock remarkDetailTitle;
        private TextBlock remarkDetailContent;
        private List<Remark> remarks = new List<Remark>();
        private Remark currentRemark;

        public RemarkManager(StackPanel remarksPanel, Border remarkDetailPanel, TextBlock remarkDetailTitle, TextBlock remarkDetailContent)
        {
            this.remarksPanel = remarksPanel;
            this.remarkDetailPanel = remarkDetailPanel;
            this.remarkDetailTitle = remarkDetailTitle;
            this.remarkDetailContent = remarkDetailContent;
        }

        public void AddRemark(string title, string content)
        {
            Remark newRemark = new Remark { Title = title, Content = content };
            remarks.Add(newRemark);
            DisplayRemarks();
        }

        public void RemoveRemark(Remark remark)
        {
            remarks.Remove(remark);
            ReloadRemarks();
        }

        public void DisplayRemarks()
        {
            remarksPanel.Children.Clear();
            foreach (var remark in remarks)
            {
                Button remarkButton = new Button
                {
                    Content = remark.Title,
                    Margin = new Thickness(5)
                };
                remarkButton.Click += (s, e) => ShowRemarkDetail(remark);
                remarksPanel.Children.Add(remarkButton);
            }

            if (remarks.Count == 0)
            {
                AddRemark("No Remarks", "There are currently no remarks available.");
            }

            remarksPanel.Visibility = Visibility.Visible;
        }

        public void ReloadRemarks()
        {
            DisplayRemarks();
        }

        private void ShowRemarkDetail(Remark remark)
        {
            currentRemark = remark;
            remarkDetailTitle.Text = remark.Title;
            remarkDetailContent.Text = remark.Content;
            remarkDetailPanel.Visibility = Visibility.Visible;
            remarkDetailPanel.Opacity = 1;
        }

        public List<Remark> GetAllRemarks()
        {
            List<Remark> allRemarks = new List<Remark>();
            for (int i = 0; i < remarks.Count; i++)
            {
                allRemarks.Add(remarks[i]);
            }
            return allRemarks;
        }

        public void CloseRemarkDetail()
        {
            remarkDetailPanel.Visibility = Visibility.Collapsed;

            if (currentRemark != null)
            {
                DeleteRemark();
            }
        }

        public void DeleteRemark()
        {
            if (currentRemark != null)
            {
                RemoveRemark(currentRemark);
                currentRemark = null;
                remarkDetailPanel.Visibility = Visibility.Collapsed;
            }
        }

        public void ClearRemarks()
        {
            remarks.Clear();
            remarksPanel.Children.Clear();
        }
    }
}
