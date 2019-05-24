using Mailarn.Data.Construct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mailarn.Data.Management
{
    public class EmailManagement
    {
        public event EventHandler ProecessSuccess;
        public delegate void ProcessFailDelegate(string Report);
        public event ProcessFailDelegate ProcessFail;
        public string firstname, lastname, emailaddress;
        public int userid, emailid, emaillistid;
        public EmailManagement(int UserId, int EmailId=0, int EmailListId = 0, string FirstName = "", string LastName = "", string EmailAddress = "")
        {
            userid = UserId;
            emailid = EmailId;
            firstname = FirstName;
            lastname = LastName;
            emailaddress = EmailAddress;
            emaillistid = EmailListId;
        }
        public void Add()
        {
            using (EmailDbContext db = new EmailDbContext())
            {
                try
                {
                    Email email = new Email()
                    {
                        F_Name = firstname,
                        L_Name = lastname,
                        Email_Add = emailaddress,
                        Eml_Id = emailid,
                        U_Id = userid
                    };
                    db.Emails.Add(email);
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
            using (EmailDbContext db = new EmailDbContext())
            {
                try
                {
                    Email email = db.Emails.Where(x => x.U_Id == userid && x.Eml_Id == emailid).FirstOrDefault();
                    db.Emails.Remove(email);
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
            using (EmailDbContext db = new EmailDbContext())
            {
                Email email = db.Emails.Where(x => x.U_Id == userid && x.Eml_Id == emailid).FirstOrDefault();
                try
                {
                    if (firstname != email.F_Name)
                    {
                        email.F_Name = firstname;
                    }
                    else if (lastname != email.L_Name)
                    {
                        email.L_Name = lastname;
                    }
                    else if (emailaddress!= "")
                    {
                        email.Email_Add = emailaddress;
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
        public Email GetEmail()
        {
            using (EmailDbContext db = new EmailDbContext())
            {
                return db.Emails.Where(x => x.U_Id == userid && x.Eml_Id == emailid).First();
            }
        }
        public List<Email> GetAllEmails()
        {
            using (EmailDbContext db = new EmailDbContext())
            {
                return db.Emails.Where(x => x.U_Id == userid).ToList();
            }
        }
    }
}
