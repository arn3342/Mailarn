using Mailarn.Data.Construct;
using Mailarn.Data.Management;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mailarn.Pages.ListPage
{
    /// <summary>
    /// Interaction logic for AddListPage.xaml
    /// </summary>
    public partial class AddListPage : Page
    {
        public int UserId, emaillistid = 0;
        public AddListPage()
        {
            InitializeComponent();
        }
        private void AddCampBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Random rand = new Random();
            if (emaillistid == 0)
            {
                emaillistid = rand.Next(100, 999);
            }
            EmailListManagement emailListManagement = new EmailListManagement(UserId, EMailIdstb.Text, emaillistid, ListNameTb.Text, ListDtlsTb.Text);
            emailListManagement.ProecessSuccess += Success;
            emailListManagement.ProcessFail += Failed;
            if (AddListBtn.DisplayText != "Update")
            { emailListManagement.Add(); }
            else { emailListManagement.Update(); }
        }
        private void Failed(string Report)
        {
            MessageBox.Show("Failed to add campaign" + Environment.NewLine + Report);
        }
        private void Success(object sender, EventArgs e)
        {
            MessageBox.Show("Campaign Added/Paused/Resumed successfully!");
        }
        private void BackBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window home = Window.GetWindow(this);
            Frame MainFrame = FindChild<Frame>(home, "MainFrame");
            MainFrame.Navigate(new EmailListPage() { UserId = UserId });
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (emaillistid != 0)
            {
                EmailListManagement emallistManagement = new EmailListManagement(UserId, EmailListId: emaillistid);
                EmailList emaillist = emallistManagement.GetList();
                ListNameTb.Text = emaillist.List_Name;
                ListDtlsTb.Text = emaillist.List_Dtls;
                EMailIdstb.Text = emaillist.Eml_Id;
                DeleteBtn.Visibility = Visibility.Visible;
            }
        }
        private void DeleteBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the campaign?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                EmailListManagement emailListManagement = new EmailListManagement(UserId, EmailListId:emaillistid);
                emailListManagement.Delete();
                Window home = Window.GetWindow(this);
                Frame MainFrame = FindChild<Frame>(home, "MainFrame");
                MainFrame.Navigate(new EmailListPage() { UserId = UserId });
            }
            else { }
        }
        private T FindChild<T>(DependencyObject parent, string childName)
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
