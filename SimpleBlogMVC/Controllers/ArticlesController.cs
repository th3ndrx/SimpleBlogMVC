using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SimpleBlogMVC.DbContext;
using SimpleBlogMVC.Models;

namespace SimpleBlogMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Articles
        public ActionResult Index(int? page)
        {
            var articles = _db.Articles.OrderByDescending(x => x.DateCreated);

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var articlesList = articles.ToPagedList(pageNumber,pageSize);

            var articlesVm = articlesList.Select(x => new ArticleViewModel(x));

            ViewBag.Articles = articlesList;

            return View(articlesVm);
        }

        // GET: Articles/Details/5
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View("Details",new ArticleViewModel(article));
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            if(!User.Identity.IsAuthenticated)
                return HttpNotFound();

            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content,DateCreated,DateLastModified")] Article article)
        {
            if (ModelState.IsValid)
            {
                _db.Articles.Add(article);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,DateCreated,DateLastModified,AuthorId")] Article article)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(article).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(new ArticleViewModel(article));
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = _db.Articles.Find(id);
            _db.Articles.Remove(article);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Articles/Generate?n=5
        [HttpGet]
        public string Generate(int n)
        {
            var articles = GenerateArticles(n);
            foreach (var a in articles)
            {
                _db.Articles.Add(a);
                _db.SaveChanges();
            }

            return "Generirao sam " + n + " članka.";
        }

        // GET: Articles/DeleteAll
        [HttpGet]
        public string DeleteAll()
        {
            var articles = _db.Articles.ToList();
            _db.Articles.RemoveRange(articles);
            _db.SaveChanges(); 

            return "OK";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static IEnumerable<Article> GenerateArticles(int n)
        {
            var dateCreated = new DateTime(2016, 01, 01);
            var articles = new List<Article>();

            for (var i = 1; i <= n; i++)
            {
                var title = "Lorem ipsum " + i.ToString();

                dateCreated = dateCreated.AddDays(2);
                var dateLastModified = new DateTime?(dateCreated);

                articles.Add(new Article
                {
                    Id = i,
                    AuthorId = "53982e38-ae09-4bb9-bbea-98554bab1ee0",
                    Title = title,
                    DateCreated = dateCreated,
                    DateLastModified = dateLastModified,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent eu sollicitudin diam. Morbi sit amet nibh nec libero rutrum fermentum eu quis dolor. Sed faucibus vestibulum lectus nec dictum. Donec eget tincidunt risus. In aliquet massa sit amet rutrum aliquam. Fusce a dapibus dui. Etiam hendrerit aliquam libero, quis consectetur massa dictum id. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum nisi tellus, pretium vel leo id, convallis accumsan orci. Fusce ut tempus quam. Nulla a odio nec justo venenatis iaculis non sit amet dui. Ut vehicula purus id ullamcorper finibus. Sed porta commodo euismod. Fusce nec ultricies orci. Phasellus rhoncus diam ut efficitur finibus. Vestibulum odio lacus, tincidunt ut lorem non, commodo semper augue. Duis tincidunt metus non tellus consequat condimentum. Phasellus ac tellus orci. Proin rutrum at urna non rutrum. Phasellus eleifend tempor mi eu sollicitudin. Donec mattis euismod aliquam. Nam massa metus, porta a odio ac, mattis sollicitudin quam. Fusce tempus ultricies est egestas facilisis. Vivamus quis blandit massa. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius imperdiet ipsum ultricies convallis. Duis non dictum justo. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec pellentesque a ipsum id porta. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris interdum ac mi vitae scelerisque. Nam ac velit id ipsum ultricies auctor. Duis non turpis in ipsum vulputate convallis sit amet a lorem. Aliquam tincidunt finibus lectus, in consequat nisl scelerisque et. Praesent consectetur lectus et ornare efficitur. Vivamus et velit sodales, volutpat neque euismod, dignissim orci. Fusce tellus lorem, porta sed sollicitudin sit amet, sodales a quam. Nam ut lobortis tellus. Etiam ultrices justo in ullamcorper tempor. Vestibulum ultricies, augue sed viverra luctus, erat nisi molestie nunc, sed porttitor ipsum diam at augue. Sed finibus sodales augue, ut pellentesque eros pharetra eget. Fusce tortor ex, auctor vitae porta vitae, viverra et velit. Proin accumsan dui mauris, quis pretium augue sagittis vel. Duis vel pretium magna. Quisque iaculis blandit dolor in malesuada. Praesent eget urna et velit blandit scelerisque. Quisque ullamcorper eget quam at placerat. Vestibulum iaculis varius dolor a porttitor. Duis faucibus erat sit amet rhoncus pulvinar. Quisque dictum, magna a tempor egestas, mauris dui fermentum nulla, quis dapibus eros ex dignissim sem. Aenean sit amet leo facilisis, semper tellus quis, luctus purus. Vivamus maximus finibus pretium. Nam maximus rutrum magna. Sed non nunc eu elit luctus consequat. Ut eleifend condimentum venenatis."
                });
            }

            return articles;
        }
    }
}
