using System.Data.Entity;

namespace Mailarn.Data.Construct
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext() : base("name=MailarnCommonDbConnection")
        {
        }
        public DbSet<Email> Emails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EmailDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
