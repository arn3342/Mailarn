namespace Mailarn.Data.Construct.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        U_Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Pass = c.String(),
                        FName = c.String(),
                        LName = c.String(),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.U_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
