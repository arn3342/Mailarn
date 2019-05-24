using System.Windows;
using Mailarn.auth;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System;
using Controlpack;
using MailKit.Net.Smtp;
using Mailarn.Data.Management;
using Mailarn.Data.Construct;

namespace Mailarn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        UserAuth uAuth = new UserAuth();
        public SignIn()
        {
            InitializeComponent();
            uAuth.AuthSuccess += SuccessLogin;
            uAuth.ErrorOccured += FailedLogin;
        }
        private void ConfigBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {        
            string[] ContainerNames = new string[] {"Company name", "Last name", "First name", "Port", "Outgoing mail server"};
            for (int i = 0; i < 5; i++)
            {
                MainStack.Children.Insert(2,new ExtendedTextbox() { DisplayText = ContainerNames[i], Margin = new Thickness(0, 13, 0, 0), Height = 32 });
            }
            MainStack.Height += 37;
        }

        private void SignInBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<string> Creds = new List<string>();
            foreach (ExtendedTextbox etb in MainStack.Children.OfType<ExtendedTextbox>())
                {
                    Creds.Add(etb.Text);
                }
            if (MainStack.Children.Count <=3)
            {
                SmtpClient smtp = uAuth.Authenticate( Creds[0], Creds[1]);
            }
            else
            {
                SmtpClient smtp = uAuth.Authenticate(Creds[0], Creds[1], Creds[2], Convert.ToInt32(Creds[3]));
                UserManagement userManagement = new UserManagement(1, Creds[0], Creds[1], Creds[4], Creds[5], Creds[6]);
                userManagement.ProcessFail += FailedToAddUser;
                userManagement.Add();
            }
        }

        private void FailedToAddUser(string Report)
        {
            MessageBox.Show(Report);
        }

        void SuccessLogin(int UserId)
        {
            Home main = new Home() { userid = UserId };
            main.Show();
            this.Close();
        }
        void FailedLogin(string ErrorMsg)
        {
            MessageBox.Show("Failed to log in!" + Environment.NewLine + ErrorMsg);
        }
    }
}
