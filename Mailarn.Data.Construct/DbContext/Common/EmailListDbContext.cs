using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailarn.Data.Construct
{
    public class EmailListDbContext : DbContext
    {
        public EmailListDbContext() : base("name=MailarnCommonDbConnection")
        {
        }
        public DbSet<EmailList> EmailLists { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EmailListDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
