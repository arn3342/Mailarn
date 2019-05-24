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

namespace Mailarn.Pages.ListPage
{
    public partial class EmailListPage : Page
    {
        public int UserId = 0;
        public EmailListPage()
        {
            InitializeComponent();
        }
        class EmlList
        {
            public string List_Name { get; set;}
            public string List_Dtls { get; set; }
            public int TotalMails { get; set; }
            public int Emlist_Id { get; set; }
            public int U_Id { get; set; }
        }
        async void PopulateEmailLists(int UserId)
        {
            EmailListManagement cm = new EmailListManagement(UserId);
            List<EmailList> emailLists = cm.GetAllLists();
            List<EmlList> emlLists = new List<EmlList>();
            foreach (EmailList emailList in emailLists)
            {
                EmlList eml = new EmlList
                {
                    List_Name = emailList.List_Name,
                    List_Dtls = emailList.List_Dtls,
                    Emlist_Id = emailList.Emlist_Id,
                    U_Id = emailList.U_Id
                };
                if (emailList.Eml_Id.Contains(","))
                {
                    eml.TotalMails = emailList.Eml_Id.Split(',').Length;
                }
                else if (emailList.Eml_Id.Length ==2)
                {
                    eml.TotalMails = 1;
                }
                emlLists.Add(eml);
            }
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => dgv.ItemsSource = emlLists));
        }
        private void ShowEmailLists(int UID, int EmListId)
        {
            Window home = Window.GetWindow(this);
            Frame MainFrame = FindChild<Frame>(home, "MainFrame");
            AddListPage addListPage = new AddListPage();
            addListPage.PageHeader.Text = "Email list details";
            addListPage.AddListBtn.DisplayText = "Update";
            addListPage.UserId = UID; addListPage.emaillistid = EmListId;
            MainFrame.Navigate(addListPage);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateEmailLists(UserId);
        }
        private void NewListBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window home = Window.GetWindow(this);
            Frame MainFrame = FindChild<Frame>(home, "MainFrame");
            MainFrame.Navigate(new AddListPage() { UserId = UserId });
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
