namespace Mailarn.Data.Construct.Migrations.EmailDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        U_Id = c.Int(nullable: false),
                        Eml_Id = c.Int(nullable: false),
                        Emlist_Id = c.Int(nullable: false),
                        Email_Add = c.String(),
                        F_Name = c.String(),
                        L_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Emails");
        }
    }
}
