using Mailarn.Pages.CampaignPage;
using Mailarn.Pages.EmailPage;
using Mailarn.Pages.ListPage;
using System.Windows;

namespace Mailarn
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public int userid;
        public Home()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CampaignPage() { UserId = userid });
        }
        private void TabControl1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MainFrame != null)
                {
                if (CampaignTab.IsSelected)
                { MainFrame.Navigate(new CampaignPage() { UserId = userid }); }
                else if (ListTab.IsSelected)
                { MainFrame.Navigate(new EmailListPage() { UserId = userid }); }
                else if (EmailTab.IsSelected)
                { MainFrame.Navigate(new EmailsPage() { UserId = userid }); }
            }
        }
    }
}
