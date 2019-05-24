using Mailarn.Data.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailarn.Data.Management
{
    public class UserManagement
    {
        #region Declaring variables and events
        public event EventHandler ProcessSuccess;
        public delegate void ProcessFailDelegate(string Report);
        public event ProcessFailDelegate ProcessFail;
        public string email, password, fname, lname, companyname;
        public int userid;
        #endregion

        #region The constructor
        public UserManagement(int UserID = 0, string Email = "", string Password = "", string FName = "", string LName ="", string CompanyName = "")
        {
            userid = UserID;
            email = Email;
            password = Password;
            fname = FName;
            lname = LName;
            companyname = CompanyName;
        }
        #endregion

        #region Performing operations
        public void Add()
        {
            using (UserDbContext db = new UserDbContext())
            {
                try
                {
                    User user = new User()
                    {
                        U_Id = userid,
                        FName = fname,
                        LName = lname,
                        Email = email,
                        Pass = password,
                        CompanyName = companyname
                    };
                    db.Users.Add(user);
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
        public void Delete()
        {
            using (UserDbContext db = new UserDbContext())
            {
                try
                {
                    User user = db.Users.Where(x => x.U_Id == userid).FirstOrDefault();
                    db.Users.Remove(user);
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
            using (UserDbContext db = new UserDbContext())
            {
                User user = db.Users.Where(x => x.U_Id == userid).FirstOrDefault();
                db.Users.Attach(user);
                try
                {
                    if (fname != user.FName)
                    {
                        user.FName = fname;
                    }
                    else if (lname != user.LName)
                    {
                        user.LName = lname;
                    }
                    else if (password != user.Pass)
                    {
                        user.Pass = password;
                    }
                    else if (companyname != user.CompanyName)
                    {
                        user.CompanyName = companyname;
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
        public User GetUser()
        {
            using (UserDbContext db = new UserDbContext())
            {
                return db.Users.Where(x => x.Email == email && x.Pass == password).First();
            }
        }
        public User GetUserById()
        {
            using (UserDbContext db = new UserDbContext())
            {
                return db.Users.Where(x => x.U_Id == userid).First();
            }
        }
        #endregion
    }
}
