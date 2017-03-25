namespace PracaInz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MyUsers", name: "Id", newName: "Id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.MyUsers", name: "Id", newName: "Id");
        }
    }
}
