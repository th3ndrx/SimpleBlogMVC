namespace SimpleBlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleModelChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String());
        }
    }
}
