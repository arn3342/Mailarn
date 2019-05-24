using Mailarn.Data.Management;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Mailarn.Data.Construct;
using System.Windows.Media;

namespace Mailarn.Pages.CampaignPage
{
    /// <summary>
    /// Interaction logic for CampaignPage.xaml
    /// </summary>
    public partial class CampaignPage : Page
    {
        public int UserId { get; set; }

        public CampaignPage()
        {
            InitializeComponent();
        }
        async void PopulateCampaigns(int UserId)
        {
            CampaignManagement cm = new CampaignManagement(UserId, DateTime.Today);
            List<Campaign> campaigns = cm.GetAllCampaigns();
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => dgv.ItemsSource = campaigns));
        }
        private void ShowCampaign(int UID, int CMID)
        {
            Window home = Window.GetWindow(this);
            Frame MainFrame = FindChild<Frame>(home, "MainFrame");
            AddCampaignPage updatecamp = new AddCampaignPage();
            updatecamp.PageHeader.Text = "Campaign details";
            updatecamp.AddCampBtn.DisplayText = "Update";
            updatecamp.UserId = UID; updatecamp.CampId = CMID;
            MainFrame.Navigate(updatecamp);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateCampaigns(UserId);
        }
        private void NewCampBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window home = Window.GetWindow(this);
            Frame MainFrame = FindChild<Frame>(home, "MainFrame");
            MainFrame.Navigate(new AddCampaignPage() { UserId = UserId });
        }
        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }
}
