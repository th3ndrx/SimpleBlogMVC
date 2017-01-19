namespace SimpleBlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleModelRequirements : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Articles", "DateLastModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "DateLastModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Articles", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
