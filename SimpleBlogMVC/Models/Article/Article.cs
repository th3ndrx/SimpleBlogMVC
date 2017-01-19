using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SimpleBlogMVC.Models
{
    public class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        [Display(AutoGenerateField = false)]
        public string AuthorId { get; set; }

        public Article()
        {
            AuthorId = HttpContext.Current.User.Identity.GetUserId();

            if (DateCreated == null)
                DateCreated = DateTime.UtcNow;

            DateLastModified = DateTime.UtcNow;
        }
    }
}