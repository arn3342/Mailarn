using System;
using System.Linq;
using Mailarn.Data.Construct;
using Mailarn.Data.Management;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Mailarn.auth;
using System.IO;
using MimeKit;
using System.Collections.Generic;
using MailKit;

namespace Mailarn.Mail.CampaignManager
{
    public class RunCampaign
    {
        #region Single Campaign
        public RunCampaign(int UserId, int CampaignID, int EmailListId)
        {
            CampaignManagement campaignmanagement = new CampaignManagement(UserId, DateTime.Now, CampaignId: CampaignID);
            Campaign campaign = campaignmanagement.GetCampaign();
            UserManagement userManagement = new UserManagement(UserId);
            User user = userManagement.GetUserById();
            string[] emailListIds = new string[] { campaign.Emlist_Id };
            if (campaignmanagement.emaillistid.Contains(','))
            {
                emailListIds = campaignmanagement.emaillistid.Split(',').ToArray();
            }
            if (!CampaignCompleted(campaign))
            {
               Parallel.For(0, emailListIds.Length, index =>
                {
                    int listId = Convert.ToInt32(emailListIds[index]);
                    EmailListManagement emailListManagement = new EmailListManagement(UserId, EmailListId: listId);
                    Email[] emails = emailListManagement.GetAllEmailsFromList();
                    UserAuth auth = new UserAuth();
                    SmtpClient client = auth.Authenticate();
                    var message = new MimeMessage();
                    List<MailboxAddress> recepients = new List<MailboxAddress>();
                    for (int i = 0; i < emails.Length; i++)
                    {
                        message.To.Add(new MailboxAddress(emails[i].F_Name, emails[i].Email_Add));
                    }
                    message.From.Add(new MailboxAddress("Arrivarn", campaign.Sender_Email));
                    message.Subject = campaign.Eml_Subject;
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Emails\\" + campaign.Tmp_Id + "XX" + campaign.Tmp_Name)
                    };
                    message.Body = bodyBuilder.ToMessageBody();
                    client.Send(message);
                    client.Disconnect(true);
               });
            };
        }
        #endregion
        #region Check campaign completion status
        private bool CampaignCompleted(Campaign campaign)
        {
            if (campaign.Completed == 1)
            { return true; }
            else { return false; }
        }
        #endregion
        #region Prepare email body for campaign
        public string EmailTemplate()
        {
            return null;
        }
        #endregion
    }
}
