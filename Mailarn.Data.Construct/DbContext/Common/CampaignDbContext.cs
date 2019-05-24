using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailarn.Data.Construct
{
    public class CampaignDbContext : DbContext
    {
        public string ConnectionString;
        public CampaignDbContext() : base("name=MailarnCommonDbConnection")
        { }
        public DbSet<Campaign> Campaigns { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CampaignDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
