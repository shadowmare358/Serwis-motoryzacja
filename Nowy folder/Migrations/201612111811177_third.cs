namespace PracaInz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MyUsers", newName: "AspNetUsers");

        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Id", newName: "UserId");

        }
    }
}
