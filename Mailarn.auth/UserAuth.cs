using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MailKit.Net;
using Cryptarn.extended;
using System.Threading.Tasks;
using MailKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using Mailarn.Data.Management;
using Mailarn.Data.Construct;

namespace Mailarn.auth
{
    public class UserAuth
    {
        public SmtpClient MailKitClient;
        public delegate void ExceptionEventHandler(string ErrorMsg);
        public event ExceptionEventHandler ErrorOccured;
        public delegate void AuthSuccessEventHandler(int UserId);
        public event AuthSuccessEventHandler AuthSuccess;
        public delegate void ProgressEventHandler(string ErrorMsg);
        public event ProgressEventHandler Progress;
        private string[] UserCacheFile;
        Crypto Cipher = new Crypto();
        public event EventHandler LoginRequired;
        string errorstring = "";
        string securitykey = "mailarn33423342";
        public UserAuth()
        {
            string TempPath = Path.GetTempPath() + "\\Mailarn\\";
            if (Directory.Exists(TempPath) && Directory.GetFiles(TempPath).Length > 0)
            {
                UserCacheFile = File.ReadAllLines(TempPath + "ucc.data");
            }
        }
        public void SignIn(string Email, string Password, string Imap = "", int iport = 0)
        {
            UserManagement userManagement = new UserManagement(Email: Email, Password: Password);
            int userId = userManagement.GetUser().U_Id;
            try
            {
                Random r = new Random();
                MailKitClient = new SmtpClient(new ProtocolLogger("smtp" + r.Next(1, 100).ToString() + ".log"));
                ProgressEventHandler pg = Progress;
                pg?.Invoke("Validating server");
                pg?.Invoke("Authenticating server");
                MailKitClient.Connect(Imap, iport, SecureSocketOptions.StartTlsWhenAvailable);
                pg?.Invoke("Signing in");
                MailKitClient.Authenticate(Email, Password);
            }
           catch(Exception ex)
            {
                errorstring = "Failed to authenticate user " + Environment.NewLine + "Details : " + ex.ToString();
                ExceptionEventHandler error = ErrorOccured;
                error?.Invoke(ex.ToString());
            }
            if (errorstring == "")
            {
                AuthSuccessEventHandler Success = AuthSuccess;
                Success?.Invoke(userId);
            }
        }
        public SmtpClient Authenticate(string email="", string pwd="", string smtpserver = "", int smtpport = 0, int ParallelCount = 0)
        {
            string TempPath = Path.GetTempPath() + "\\Mailarn\\";
            
            if (Directory.Exists(TempPath) && Directory.GetFiles(TempPath).Length > 0)
            {
                email = Email(); pwd = Password(); smtpserver = SMTP(); smtpport = SMTPPort();
                SignIn(email, pwd, smtpserver, smtpport);
            }
            else
            {
                Random r = new Random();
                MailKitClient = new SmtpClient(new ProtocolLogger("smtp" + r.Next(1, 100).ToString() + ".log"));
                if (email != "")
                {
                    Directory.CreateDirectory(TempPath);
                    File.WriteAllLines(TempPath + "ucc.data", new string[]{ Cipher.Encrypt(email, securitykey),
                    Cipher.Encrypt(pwd, securitykey),
                    Cipher.Encrypt(smtpserver, securitykey),
                    Cipher.Encrypt(smtpport.ToString() , securitykey) });
                    SignIn(email, pwd, smtpserver, smtpport);
                    try
                    {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress("Mailarn Test", email));
                            message.To.Add(new MailboxAddress("Test", email));
                            message.Subject = "A test email";
                            message.Body = new TextPart("plain")
                            {
                                Text = @"This is a test email sent from Mailarn E-Marketing software."
                            };
                        if (MailKitClient.IsConnected)
                        {
                            MailKitClient.Disconnect(true);
                        }
                        MailKitClient.Connect(smtpserver, smtpport, SecureSocketOptions.StartTls);
                        MailKitClient.Authenticate(email, pwd);
                        MailKitClient.Send(message);
                        MailKitClient.Disconnect(true);
                    }
                    catch (Exception ex)
                    {
                        errorstring = "Failed to send test email " + Environment.NewLine + "Details : " + ex.ToString();
                    }
                    if (errorstring == "")
                    {
                        UserManagement userManagement = new UserManagement(Email: email, Password: pwd);
                        int userId = userManagement.GetUser().U_Id;
                        AuthSuccessEventHandler Success = AuthSuccess;
                        Success?.Invoke(userId);
                    }
                }
                else
                {
                EventHandler LoginReq = LoginRequired;
                LoginReq?.Invoke(null, null);
                ProgressEventHandler Login = Progress;
                Login?.Invoke("Initializing");
                }
            }
            return MailKitClient;
        }
        #region Decrypting user data
        public string Email()
        {
            return Cipher.Decrypt(UserCacheFile[0], securitykey);
        }
        public string Password()
        {
            return Cipher.Decrypt(UserCacheFile[1], securitykey);
        }
        public string SMTP()
        {
            return Cipher.Decrypt(UserCacheFile[2], securitykey);
        }
        public int SMTPPort()
        {
            return Convert.ToInt32(Cipher.Decrypt(UserCacheFile[3], securitykey));
        }
        #endregion
    }
}
