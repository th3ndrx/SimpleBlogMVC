using System.Linq;
using System.Web.Mvc;
using SimpleBlogMVC.DbContext;

namespace SimpleBlogMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(_db.Articles.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}