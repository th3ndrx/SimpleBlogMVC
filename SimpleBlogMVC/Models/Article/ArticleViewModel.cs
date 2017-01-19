using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleBlogMVC.DbContext;

namespace SimpleBlogMVC.Models
{
    public class ArticleViewModel
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentPreview{ get; set; }
        public string Author { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

        public ArticleViewModel(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Content;
            DateCreated = article.DateCreated;
            DateLastModified = article.DateLastModified;
            

            if (Content != null)
            {
                string contentTemp = System.Net.WebUtility.HtmlDecode(Content.Substring(0, Math.Min(Content.Length, 450)) + "...");
                ContentPreview = StripHtml(contentTemp);
            }

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(article.AuthorId);

            if(user?.UserName != null)
                Author = user.UserName;

        }

        public static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

    }
}