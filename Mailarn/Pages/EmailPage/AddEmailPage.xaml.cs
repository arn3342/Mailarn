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

namespace Mailarn.Pages.EmailPage
{
    /// <summary>
    /// Interaction logic for AddEmailPage.xaml
    /// </summary>
    public partial class AddEmailPage : Page
    {
        public int UserId, emailid = 0;
        public AddEmailPage()
        {
            InitializeComponent();
        }
        private void AddCampBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Random rand = new Random();
            if (emailid == 0)
            {
                emailid = rand.Next(100, 999);
            }
            EmailManagement emailManagement = new EmailManagement(UserId, emailid, 0, FNameTb.Text, LNameTb.Text, EMailtb.Text);
            emailManagement.ProecessSuccess += Success;
            emailManagement.ProcessFail += Failed;
            if (AddListBtn.DisplayText != "Update")
            { emailManagement.Add(); }
            else { emailManagement.Update(); }
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
            MainFrame.Navigate(new EmailsPage() { UserId = UserId });
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (emailid != 0)
            {
                EmailManagement emallManagement = new EmailManagement(UserId, EmailId:emailid);
                Email email = emallManagement.GetEmail();
                FNameTb.Text = email.F_Name;
                LNameTb.Text = email.L_Name;
                EMailtb.Text = email.Email_Add;
                DeleteBtn.Visibility = Visibility.Visible;
            }
        }
        private void DeleteBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the email address?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                EmailManagement emailManagement = new EmailManagement(UserId, EmailId: emailid);
                emailManagement.Delete();
                Window home = Window.GetWindow(this);
                Frame MainFrame = FindChild<Frame>(home, "MainFrame");
                MainFrame.Navigate(new EmailsPage() { UserId = UserId });
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
