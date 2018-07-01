
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

            var result = db.Paintings.Where(p => p.Name.Contains(SearchString)
                                       || p.Description.Contains(SearchString)).ToList();

            var paintersIds = db.Painters.Where(p => p.Name.Contains(SearchString)).Select(p => p.ID).ToList();

            foreach (var painterId in paintersIds)
            {
                var painting = db.Paintings.Where(p => p.PainterID == painterId).FirstOrDefault();
                if (painting != null)
                    result.Add(painting);
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            result.OrderBy(p => p.Name);

            return View(result.ToPagedList(pageNumber, pageSize));
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