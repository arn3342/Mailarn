using Mailarn.Data.Construct;
using Mailarn.Data.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mailarn.Pages.EmailPage
{
    /// <summary>
    /// Interaction logic for EmailsPage.xaml
    /// </summary>
    public partial class EmailsPage : Page
    {
        public int UserId = 0;
        public EmailsPage()
        {
            InitializeComponent();
        }
        async void PopulateEmailLists(int UserId)
        {
            EmailManagement emailManagement = new EmailManagement(UserId);
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => dgv.ItemsSource = emailManagement.GetAllEmails()));
        }
        private void ShowEmail(int UID, int EmailId)
        {
            Window home = Window.GetWindow(this);
            Frame MainFrame = FindChild<Frame>(home, "MainFrame");
            AddEmailPage addemailPage = new AddEmailPage();
            addemailPage.PageHeader.Text = "Email list details";
            addemailPage.AddListBtn.DisplayText = "Update";
            addemailPage.UserId = UID; addemailPage.emailid = EmailId;
            MainFrame.Navigate(addemailPage);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateEmailLists(UserId);
        }
        private void NewEmailBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window home = Window.GetWindow(this);
            Frame MainFrame = FindChild<Frame>(home, "MainFrame");
            MainFrame.Navigate(new AddEmailPage() { UserId = UserId });
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

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            ShowEmail(UserId, Convert.ToInt32(tb.Text));
        }
    }
}
