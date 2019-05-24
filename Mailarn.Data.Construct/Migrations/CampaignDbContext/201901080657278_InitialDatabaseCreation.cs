namespace Mailarn.Data.Construct.Migrations.CampaignDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        U_Id = c.Int(nullable: false),
                        Camp_Id = c.Int(nullable: false),
                        Camp_Name = c.String(),
                        Camp_Dtls = c.String(),
                        Camp_Date = c.DateTime(nullable: false),
                        Camp_ACStat = c.String(),
                        Eml_Subject = c.String(),
                        Emlist_Id = c.String(),
                        Sender_Email = c.String(),
                        Tmp_Id = c.Int(nullable: false),
                        Tmp_Name = c.String(),
                        Tmp_Body = c.String(),
                        Sentmail_Count = c.Int(nullable: false),
                        Ld_Count = c.Int(nullable: false),
                        Succ_Rt = c.Int(nullable: false),
                        Completed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Campaigns");
        }
    }
}
