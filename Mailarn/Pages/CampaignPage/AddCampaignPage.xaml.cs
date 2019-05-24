using Mailarn.Data.Construct;
using Mailarn.Data.Management;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mailarn.Pages.CampaignPage
{
    /// <summary>
    /// Interaction logic for AddCampaignPage.xaml
    /// </summary>
    public partial class AddCampaignPage : Page
    {
        public int UserId,CampId = 0;
        public AddCampaignPage()
        {
            InitializeComponent();
        }
        private void AddCampBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Random rand = new Random();
            if (CampId == 0)
            {
                CampId = rand.Next(100, 999);
            }
            string[] tmpnameid = TemplatesTb.Text.Split(new string[] { "XX" }, StringSplitOptions.None);
            string test = tmpnameid[0];
            CampaignManagement campaignManagement = new CampaignManagement(UserId, CampDate.DisplayDate, CampaignNameTb.Text, CampId, CampaignDtlsTb.Text, "Active", ListTb.Text, SubjectTb.Text, SenderMailtb.Text, Convert.ToInt32(tmpnameid[0]), tmpnameid[1] );
            campaignManagement.ProcessSuccess += Success;
            campaignManagement.ProcessFail += Failed;
            if (AddCampBtn.DisplayText != "Update")
            { campaignManagement.Add(); }
            else { campaignManagement.Update(); }
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
            MainFrame.Navigate(new CampaignPage() { UserId = UserId});
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (CampId !=0)
            {
                CampaignManagement campaignManagement = new CampaignManagement(UserId, DateTime.Today, CampaignId: CampId);
                Campaign camp = campaignManagement.GetCampaign();
                CampaignNameTb.Text = camp.Camp_Name;
                CampaignDtlsTb.Text = camp.Camp_Dtls;
                ListTb.Text = camp.Emlist_Id;
                TemplatesTb.Text = camp.Tmp_Id.ToString();
                Status.Text = camp.Camp_ACStat;
                StatGrid.Visibility = Visibility.Visible;
                CampDate.SelectedDate = camp.Camp_Date;
                PauseBtn.Visibility = Visibility.Visible; DeleteBtn.Visibility = Visibility.Visible;
                if (camp.Camp_ACStat == "Active")
                {
                    PauseBtn.DisplayText = "Pause";
                    PauseBtn.ImgSource = new BitmapImage(
    new Uri("pack://application:,,,/Mailarn;component/icos/pause.png"));
                }
                else
                {
                    PauseBtn.DisplayText = "Resume";
                    PauseBtn.ImgSource = new BitmapImage(
    new Uri("pack://application:,,,/Mailarn;component/icos/play.png"));
                }
            }
        }
        private void PauseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to pause/resume the campaign?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (Status.Text != "Active")
                {
                    CampaignManagement campaignManagement = new CampaignManagement(UserId, DateTime.Today, CampaignId: CampId, CampaignStatus: "Active");
                    campaignManagement.ProcessSuccess += Success;
                    campaignManagement.ProcessFail += Failed;
                    campaignManagement.Update();
                    PauseBtn.DisplayText = "Resume";
                    PauseBtn.ImgSource = new BitmapImage(
                    new Uri("pack://application:,,,/Mailarn;component/icos/play.png"));
                }
                else
                {
                    CampaignManagement campaignManagement = new CampaignManagement(UserId, DateTime.Today, CampaignId: CampId, CampaignStatus: "Paused");
                    campaignManagement.ProcessSuccess += Success;
                    campaignManagement.ProcessFail += Failed;
                    campaignManagement.Update();
                    PauseBtn.DisplayText = "Pause";
                    PauseBtn.ImgSource = new BitmapImage(
        new Uri("pack://application:,,,/Mailarn;component/icos/pause.png"));
                }
            }
            else
            {
                //Nothing
            }
            
        }
        private void DeleteBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the campaign?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                CampaignManagement campaignManagement = new CampaignManagement(UserId, DateTime.Today, CampaignId: CampId);
                campaignManagement.Delete();
                Window home = Window.GetWindow(this);
                Frame MainFrame = FindChild<Frame>(home, "MainFrame");
                MainFrame.Navigate(new CampaignPage() { UserId = UserId });
            }
            else { }
        }
        private void TemplatesTb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TemplatesTb.Text = openFileDialog.SafeFileName;
            }
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
