using Mailarn.Data.Construct;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Mailarn.Data.Management
{
    public class CampaignManagement
    {
        public event EventHandler ProcessSuccess;
        public delegate void ProcessFailDelegate(string Report);
        public event ProcessFailDelegate ProcessFail;
        public DateTime startdate;
        public string campaigndetails, campaignname, emaillistid, campaignstatus, subject, senderemail, templatename, templatebody;
        public int userid, campaignid, templateid, sentemailcount;
        public CampaignManagement(int UserId = 0, DateTime StartDate = default(DateTime), string CampaignName = "", int CampaignId = 0, string CampaignDetails = "", string CampaignStatus = "Active", string EmailListId = "", string Subject = "", string SenderEmail = "", int TemplateId = 0, string TemplateName = "", string TemplateBody = "", int SentEmailCount = 0)
        {
            startdate = StartDate;
            userid = UserId;
            campaignname = CampaignName;
            campaignid = CampaignId;
            campaigndetails = CampaignDetails;
            campaignstatus = CampaignStatus;
            subject = Subject;
            senderemail = SenderEmail;
            templatebody = TemplateBody;
            templatename = TemplateName;
            emaillistid = EmailListId;
            templateid = TemplateId;
            sentemailcount = SentEmailCount;
        }
        public void Add()
        {
            using (CampaignDbContext db = new CampaignDbContext())
            {
                try
                {
                    Campaign campaign = new Campaign()
                    {
                        Camp_Id = campaignid,
                        U_Id = userid,
                        Camp_Dtls = campaigndetails,
                        Camp_ACStat = campaignstatus,
                        Emlist_Id = emaillistid,
                        Sentmail_Count = sentemailcount,
                        Camp_Name = campaignname,
                        Tmp_Id = templateid,
                        Camp_Date = startdate,
                        //Tmp_Body = templatebody,
                        Tmp_Name = templatename,
                        Eml_Subject = subject,
                        Sender_Email = senderemail
                    };
                    db.Campaigns.Add(campaign);
                    db.SaveChangesAsync();
                    ProcessSuccess?.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    ProcessFailDelegate Failed = ProcessFail;
                    Failed?.Invoke("Failed" + Environment.NewLine + ex.ToString());
                }
            }
        }
        public void Delete()
        {
            using (CampaignDbContext db = new CampaignDbContext())
            {
                try
                {
                    Campaign campaign = db.Campaigns.Where(x => x.U_Id == userid && x.Camp_Id == campaignid).FirstOrDefault();
                    db.Campaigns.Remove(campaign);
                    db.SaveChanges();
                    ProcessSuccess?.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    ProcessFailDelegate Failed = ProcessFail;
                    Failed?.Invoke("Failed" + Environment.NewLine + ex.ToString());
                }
            }
        }
        public void Update()
        {  
            using (CampaignDbContext db = new CampaignDbContext())
            {
                Campaign campaign = db.Campaigns.Where(x => x.U_Id == userid && x.Camp_Id == campaignid).FirstOrDefault();
                db.Campaigns.Attach(campaign);
                try
                {
                    if (campaignstatus != campaign.Camp_ACStat)
                    {
                        campaign.Camp_ACStat = campaignstatus;
                    }
                    else if (campaigndetails != campaign.Camp_Dtls)
                    {
                        campaign.Camp_Dtls = campaigndetails;
                    }
                    else if (emaillistid != campaign.Emlist_Id)
                    {
                        campaign.Emlist_Id = emaillistid;
                    }
                    else if (campaignname != campaign.Camp_Name)
                    {
                        campaign.Camp_Name = campaignname;
                    }
                    else if (templateid != campaign.Tmp_Id)
                    {
                        campaign.Tmp_Id = templateid;
                    }
                    else if (sentemailcount != campaign.Sentmail_Count)
                    {
                        campaign.Sentmail_Count = sentemailcount;
                    }
                    else if (startdate != campaign.Camp_Date)
                    {
                        campaign.Camp_Date = startdate;
                    }
                    else if (senderemail != campaign.Sender_Email)
                    {
                        campaign.Sender_Email = senderemail;
                    }
                    else if (templatename != campaign.Tmp_Name)
                    {
                        campaign.Tmp_Name = templatename;
                    }
                    //else if (templatebody != campaign.Tmp_Body)
                    //{
                    //    campaign.Tmp_Body = templatebody;
                    //}
                    else if (subject != campaign.Eml_Subject)
                    {
                        campaign.Eml_Subject = subject;
                    }
                    db.SaveChangesAsync();
                    ProcessSuccess?.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    ProcessFailDelegate Failed = ProcessFail;
                    Failed?.Invoke("Failed" + Environment.NewLine + ex.ToString());
                }
            }
        }
        public Campaign GetCampaign()
        {
            using (CampaignDbContext db = new CampaignDbContext())
            {
                return db.Campaigns.Where(x => x.U_Id == userid && x.Camp_Id == campaignid).First();
            }
        }
        public List<Campaign> GetAllCampaigns()
        { 
            using (CampaignDbContext db = new CampaignDbContext())
            {
               return db.Campaigns.Where(x => x.U_Id == userid).ToList();
            }
        }
    }
}
