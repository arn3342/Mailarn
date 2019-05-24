using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Mailarn.auth;

namespace Mailarn
{
    public partial class Starting : Window
    {
        UserAuth Authentication = new UserAuth();
        BackgroundWorker ProcessAuth = new BackgroundWorker();
        public Starting()
        {
            InitializeComponent();
            ProcessAuth.DoWork += new DoWorkEventHandler(RunAuthentication);
            Authentication.AuthSuccess += AuthResult;
            Authentication.ErrorOccured += Error;
            Authentication.Progress += ShowProgress;
            Authentication.LoginRequired += ShowLogin;
        }

        private void Error(string ErrorMsg)
        {
            MessageBox.Show(ErrorMsg);
        }

        private async void AuthResult(int UserId)
        {
            await Task.Delay(300);
            Home home = new Home() { userid = UserId };
            home.Show();
            this.Hide();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(200);
            ProcessAuth.RunWorkerAsync();
        }
        private void RunAuthentication(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => Authentication.Authenticate()));
        }
        void ShowProgress(string Report)
        {
            Loading.DisplayText = Report;   
        }
        async void ShowLogin(object sender, EventArgs e)
        {
            await Task.Delay(300);
            SignIn signin = new SignIn();
            signin.Show();
            this.Hide();
        }
    }
}
