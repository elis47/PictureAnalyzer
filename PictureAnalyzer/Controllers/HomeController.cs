
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PictureAnalyzer.Models;
using System.IO;

namespace PictureAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchString, int? page)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Search", new { SearchString = searchString, page = 1 });
            }
            else return View();
        }

        public ActionResult Search(string SearchString, int? page)
        {
            ViewBag.CurrentFilter = SearchString;

            var galleries = from g in db.Galleries
                            select g;
            var painters = from p in db.Painters
                           select p;

            painters = painters.Where(p => p.Name.Contains(SearchString)
                                       || p.Description.Contains(SearchString));
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            painters = painters.OrderBy(p => p.Name);

            return View(painters.ToPagedList(pageNumber, pageSize));
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