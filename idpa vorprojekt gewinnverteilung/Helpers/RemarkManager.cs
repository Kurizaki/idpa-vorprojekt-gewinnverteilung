namespace idpa_vorprojekt_gewinnverteilung.Helpers;

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

public class RemarkManager
{
    private List<Remark> remarks = new List<Remark>();
    private StackPanel remarksPanel;
    private Border remarkDetailPanel;
    private TextBlock remarkDetailTitle;
    private TextBlock remarkDetailContent;

    public RemarkManager(StackPanel remarksPanel, Border remarkDetailPanel, TextBlock remarkDetailTitle, TextBlock remarkDetailContent)
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
                Background = new SolidColorBrush(Colors.White),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(5),
                Padding = new Thickness(10),
                Child = new TextBlock { Text = remark.Title, FontWeight = FontWeights.Bold }
            };

            remarkControl.MouseLeftButtonUp += (s, e) => ShowRemarkDetail(remark);
            remarksPanel.Children.Add(remarkControl);
        }
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
            remarkDetailPanel.Visibility = Visibility.Collapsed;
            var remark = remarks.Find(r => r.Title == remarkDetailTitle.Text);
            if (remark != null)
            {
                RemoveRemark(remark);
            }
        };
        remarkDetailPanel.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
    }
}

public class Remark
{
    public string Title { get; set; }
    public string Content { get; set; }
}
