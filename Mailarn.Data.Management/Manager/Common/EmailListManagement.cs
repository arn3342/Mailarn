using System;
using System.Collections.Generic;
using System.Linq;
using Mailarn.Data.Construct;
using System.Data.Entity;
using System.Collections;

namespace Mailarn.Data.Management
{
    public class EmailListManagement
    {
        public event EventHandler ProecessSuccess;
        public delegate void ProcessFailDelegate(string Report);
        public event ProcessFailDelegate ProcessFail;
        public string listname, listdetails, emailid;
        public int userid, emaillistid;
        public EmailListManagement(int UserId, string EmailId = "", int EmailListId = 0, string ListName = "", string ListDetails = "")
        {
            userid = UserId;
            emailid = EmailId;
            listdetails = ListDetails;
            listname = ListName;
            emaillistid = EmailListId;
        }
        public void Add()
        {
            using (EmailListDbContext db = new EmailListDbContext())
            {
                try
                {
                    EmailList emaillist = new EmailList()
                    {
                        List_Name = listname,
                        List_Dtls = listdetails,
                        Eml_Id = emailid,
                        Emlist_Id = emaillistid,
                        U_Id = userid
                    };
                    db.EmailLists.Add(emaillist);
                    db.SaveChangesAsync();
                    ProecessSuccess?.Invoke(null, null);
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
            using (EmailListDbContext db = new EmailListDbContext())
            {
                try
                {
                    EmailList emaillist = db.EmailLists.Where(x => x.U_Id == userid && x.Emlist_Id == emaillistid).FirstOrDefault();
                    db.EmailLists.Remove(emaillist);
                    db.SaveChangesAsync();
                    ProecessSuccess?.Invoke(null, null);
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
            using (EmailListDbContext db = new EmailListDbContext())
            {
                EmailList emaillist = db.EmailLists.Where(x => x.U_Id == userid && x.Emlist_Id == emaillistid).FirstOrDefault();
                try
                {
                    if (listname != emaillist.List_Name)
                    {
                        emaillist.List_Name = listname;
                    }
                    else if (listdetails != emaillist.List_Dtls)
                    {
                        emaillist.List_Dtls = listdetails;
                    }
                    else if (emailid != emaillist.Eml_Id)
                    {
                        emaillist.Eml_Id = emailid;
                    }
                    db.SaveChanges();
                    ProecessSuccess?.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    ProcessFailDelegate Failed = ProcessFail;
                    Failed?.Invoke("Failed" + Environment.NewLine + ex.ToString());
                }
            }
        }
        public Email[] GetAllEmailsFromList()
        {
            using (EmailDbContext emaildb = new EmailDbContext())
            {
                return emaildb.Emails.Where(x => x.U_Id == userid && x.Emlist_Id == emaillistid).ToArray();
            }
        }
        public string[] GetEmailAddresses()
        {
            using (EmailDbContext emaildb = new EmailDbContext())
            {
                return emaildb.Emails.Where(x => x.U_Id == userid && x.Emlist_Id == emaillistid).Select(x => x.Email_Add).ToArray();
            }
        }
        public List<EmailList> GetAllLists()
        {
            using (EmailListDbContext db = new EmailListDbContext())
            {
                return db.EmailLists.Where(x => x.U_Id == userid).ToList();
            }
        }
        public EmailList GetList()
        {
            using (EmailListDbContext db = new EmailListDbContext())
            {
                return db.EmailLists.Where(x => x.U_Id == userid &&  x.Emlist_Id == emaillistid).FirstOrDefault();
            }
        }
    }
}
