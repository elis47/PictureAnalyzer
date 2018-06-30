using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PictureAnalyzer.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using Newtonsoft.Json;
using EmguCV;
using PagedList;

namespace PictureAnalyzer.Controllers
{
    public class PaintingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paintings
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "description_desc" : "Description";
            ViewBag.InfluenceSortParm = sortOrder == "Influence" ? "influence_desc" : "Influence";
            ViewBag.PainterSortParm = sortOrder == "Painter" ? "painter_desc" : "Painter";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var paintings = from p in db.Paintings
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                paintings = paintings.Where(p => p.Name.Contains(searchString)
                                       || p.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    paintings = paintings.OrderByDescending(p => p.Name);
                    break;
                case "Description":
                    paintings = paintings.OrderBy(p => p.Description);
                    break;
                case "description_desc":
                    paintings = paintings.OrderByDescending(p => p.Description);
                    break;
                case "Influence":
                    paintings = paintings.OrderBy(p => p.Influence);
                    break;
                case "influence_desc":
                    paintings = paintings.OrderByDescending(p => p.Influence);
                    break;
                case "Painter":
                    paintings = paintings.OrderBy(p => p.Painter);
                    break;
                case "painter_desc":
                    paintings = paintings.OrderByDescending(p => p.Painter);
                    break;
                default:
                    paintings = paintings.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(paintings.ToPagedList(pageNumber, pageSize));
        }

        // GET: Paintings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            return View(painting);
        }

        // GET: Paintings/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.GalleryID = new SelectList(db.Galleries, "ID", "Name");
            ViewBag.InfluenceID = new SelectList(db.Influences, "ID", "Name");
            ViewBag.PainterID = new SelectList(db.Painters, "ID", "Name");
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name");
            return View();
        }

        // GET: Paintings/ProfileResult
        public ActionResult ProfileResult()
        {
            return View();
        }

        // POST: Paintings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Painting painting, HttpPostedFileBase file)
        {
            var currentUserId = User.Identity.GetUserId();
            painting.ApplicationUserId = currentUserId;

            var currentUser = db.Users.FirstOrDefault(u => u.Id == currentUserId);
            painting.ApplicationUser = currentUser;

            byte[] uploadedFile = new byte[painting.File.InputStream.Length];
            painting.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string pic = Path.GetFileName(file.FileName);
                    string physicalPath = Path.Combine(Server.MapPath("~/Images/"), pic);

                    file.SaveAs(physicalPath);
                    painting.Link = "/Images/" + file.FileName;

                    /* Image processing part */

                    ImageProcessing i = new ImageProcessing(physicalPath);

                    Dictionary<Pixel, int> colorDistribution = i.GetColorDistribution(physicalPath);
                    colorDistribution = i.GetSortedDictionary(colorDistribution);
                    colorDistribution = i.GetTopRecords(colorDistribution);

                    double harmonicMeanAll = i.GetHarmonicMeanForAllColors();
                    double harmonicMeanDominant = i.GetHarmonicMeanForDominantColors();
                    double harmonicMinorityMean = i.GetHarmonicMeanForMinorityColors();

                    double finalHarmonicMean = (harmonicMeanAll + harmonicMeanDominant + harmonicMinorityMean) / 3;

                    List<string> colorsKeywords = new List<string>();
                    var colors = db.Colors;
                    var ourProfile = "";

                    foreach (var color in colorDistribution)
                    {
                        var searchedColor = colors.Where(c => c.Name == color.Key.Color).FirstOrDefault();
                        if (searchedColor != null)
                        {
                            foreach (var keyword in searchedColor.Keywords.Split(','))
                            {
                                colorsKeywords.Add(keyword);
                            }
                            painting.Colors.Add(searchedColor);
                        }
                    }

                    if (colorDistribution.Count > 2)
                    {
                        var key1 = colorDistribution.Keys.Last();
                        colorDistribution.Remove(key1);
                        var key2 = colorDistribution.Keys.Last();

                        var description1 = colors.Where(c => c.Name == key1.Color).First().PersonalityTraits;
                        var description2 = colors.Where(c => c.Name == key2.Color).First().PersonalityTraits;

                        if (description1 != description2)
                            ourProfile = description1 + description2;
                        else
                            ourProfile = description1;
                    }

                    colorsKeywords.Distinct();

                    string[] typeA_Keywords = db.Profiles.Where(p => p.Name.Contains("Type A Personality")).FirstOrDefault().Keywords.Split(',');
                    string[] typeB_Keywords = db.Profiles.Where(p => p.Name.Contains("Type B Personality")).FirstOrDefault().Keywords.Split(',');
                    string[] typeC_Keywords = db.Profiles.Where(p => p.Name.Contains("Type C Personality")).FirstOrDefault().Keywords.Split(',');
                    string[] typeD_Keywords = db.Profiles.Where(p => p.Name.Contains("Type D Personality")).FirstOrDefault().Keywords.Split(',');

                    int typeA_MatchNumber = 0;
                    int typeB_MatchNumber = 0;
                    int typeC_MatchNumber = 0;
                    int typeD_MatchNumber = 0;

                    foreach (var keyword in colorsKeywords)
                    {
                        foreach (var typeAkeyword in typeA_Keywords)
                        {
                            if (String.Equals(typeAkeyword, keyword))
                                typeA_MatchNumber++;
                        }
                        foreach (var typeBkeyword in typeB_Keywords)
                        {
                            if (String.Equals(typeBkeyword, keyword))
                                typeB_MatchNumber++;
                        }
                        foreach (var typeCkeyword in typeC_Keywords)
                        {
                            if (String.Equals(typeCkeyword, keyword))
                                typeC_MatchNumber++;
                        }
                        foreach (var typeDkeyword in typeD_Keywords)
                        {
                            if (String.Equals(typeDkeyword, keyword))
                                typeD_MatchNumber++;
                        }
                    }

                    int totalMatches = typeA_MatchNumber + typeB_MatchNumber + typeC_MatchNumber + typeD_MatchNumber;

                    double typeA_Percentage = (double)typeA_MatchNumber / totalMatches;
                    double typeB_Percentage = (double)typeB_MatchNumber / totalMatches;
                    double typeC_Percentage = (double)typeC_MatchNumber / totalMatches;
                    double typeD_Percentage = (double)typeD_MatchNumber / totalMatches;

                    painting.TypeAPercentage = Convert.ToDouble(String.Format("{0:0.00}", typeA_Percentage)) * 100;
                    painting.TypeBPercentage = Convert.ToDouble(String.Format("{0:0.00}", typeB_Percentage)) * 100;
                    painting.TypeCPercentage = Convert.ToDouble(String.Format("{0:0.00}", typeC_Percentage)) * 100;
                    painting.TypeDPercentage = Convert.ToDouble(String.Format("{0:0.00}", typeD_Percentage)) * 100;

                    ViewBag.TypeAPercentage = Convert.ToDouble(String.Format("{0:0.00}", typeA_Percentage)) * 100;
                    ViewBag.TypeBPercentage = Convert.ToDouble(String.Format("{0:0.00}", typeB_Percentage)) * 100;
                    ViewBag.TypeCPercentage = Convert.ToDouble(String.Format("{0:0.00}", typeC_Percentage)) * 100;
                    ViewBag.TypeDPercentage = Convert.ToDouble(String.Format("{0:0.00}", typeD_Percentage)) * 100;

                    var profiles = db.Profiles.ToList();
                    painting.Profiles = profiles;

                    ViewBag.TypeADescription = profiles.ElementAt(0).Description;
                    ViewBag.TypeBDescription = profiles.ElementAt(1).Description;
                    ViewBag.TypeCDescription = profiles.ElementAt(2).Description;
                    ViewBag.TypeDDescription = profiles.ElementAt(3).Description;

                    double interval = 1.24 + 1.39;

                    ViewBag.AllHarmonyIndex = Convert.ToDouble(String.Format("{0:0.00}", (1.24 + harmonicMeanAll) / interval)) * 100;
                    ViewBag.DominantHarmonyIndex = Convert.ToDouble(String.Format("{0:0.00}", (1.24 + harmonicMeanDominant) / interval)) * 100;
                    ViewBag.MinorHarmonyIndex = Convert.ToDouble(String.Format("{0:0.00}", (1.24 + harmonicMinorityMean) / interval)) * 100;

                    ViewBag.OurProfile = ourProfile;
                }
            }

            if (painting.Description == null)
                painting.Description = "No description available";
            if (painting.CurrentOwner == null)
                painting.CurrentOwner = "N/A";

            painting.ApplicationUserId = User.Identity.GetUserId();

            db.Paintings.Add(painting);
            db.SaveChanges();

            return View("ProfileResult", painting);
        }

        // GET: Paintings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", painting.ApplicationUserId);
            ViewBag.GalleryID = new SelectList(db.Galleries, "ID", "Name", painting.GalleryID);
            ViewBag.InfluenceID = new SelectList(db.Influences, "ID", "Name", painting.InfluenceID);
            ViewBag.PainterID = new SelectList(db.Painters, "ID", "Name", painting.PainterID);
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", painting.TypeID);
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,CurrentOwner,HarmonyIndex,ConstrastIndex,LuminosityIndex,Link,ApplicationUserId,PainterID,TypeID,InfluenceID,GalleryID")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(painting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", painting.ApplicationUserId);
            ViewBag.GalleryID = new SelectList(db.Galleries, "ID", "Name", painting.GalleryID);
            ViewBag.InfluenceID = new SelectList(db.Influences, "ID", "Name", painting.InfluenceID);
            ViewBag.PainterID = new SelectList(db.Painters, "ID", "Name", painting.PainterID);
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", painting.TypeID);
            return View(painting);
        }

        // GET: Paintings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            return View(painting);
        }

        // POST: Paintings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Painting painting = db.Paintings.Find(id);
            db.Paintings.Remove(painting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
