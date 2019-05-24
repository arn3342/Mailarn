using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailarn.Data.Construct
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() : base("name=MailarnMasterDbConnection")
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<UserDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
