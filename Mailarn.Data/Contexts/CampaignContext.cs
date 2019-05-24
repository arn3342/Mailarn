using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailarn.Data
{
    public class CampaignContext : DbContext
    {
        public CampaignContext(string constring) 
            : base(constring)
        { }
        public DbSet<Campaign> Campaigns { get; set; }
    }
}
