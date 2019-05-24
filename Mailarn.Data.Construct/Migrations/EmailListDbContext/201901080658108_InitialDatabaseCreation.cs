namespace Mailarn.Data.Construct.Migrations.EmailListDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        U_Id = c.Int(nullable: false),
                        Emlist_Id = c.Int(nullable: false),
                        List_Name = c.String(),
                        List_Dtls = c.String(),
                        Eml_Id = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailLists");
        }
    }
}
