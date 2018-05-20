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
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;

namespace PictureAnalyzer.Controllers
{
    public class PaintingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paintings
        public ActionResult Index()
        {
            var paintings = db.Paintings.Include(p => p.ApplicationUser).Include(p => p.Gallery).Include(p => p.Influence).Include(p => p.Painter).Include(p => p.Profile).Include(p => p.Type);
            return View(paintings.ToList());
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
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Name");
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name");
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
                    string relativePath = "~/Images/" + Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath(relativePath);
                    file.SaveAs(physicalPath);

                    painting.Link = physicalPath;

                    /* Image processing part */

                    Image<Bgr, Byte> image = new Image<Bgr, byte>(physicalPath);

                    //Convert to Bitmap
                    var imageBitmap = image.ToBitmap();

                    //Convert to gray
                    var grayImage = image.Convert<Gray, byte>();

                    // Build matrix
                    Matrix<Byte> matrix = new Matrix<Byte>(image.Rows, image.Cols, image.NumberOfChannels);

                    // Build Lab image
                    Image<Lab, Byte> labImage = new Image<Lab, Byte>(image.Cols, image.Rows);

                    for (int i = 0; i < labImage.Rows; i++)
                        for (int j = 0; j < labImage.Cols; j++)
                        {
                            var pixel = labImage[i, j];

                            var x = pixel.X;
                            var y = pixel.Y;
                            var z = pixel.Z;

                            var dim = pixel.Dimension;
                        }


                    // Build Bitmap 
                    Bitmap bmap = new Bitmap(physicalPath);

                    for (int i = 0; i < bmap.Width; i++)
                    {
                        for (int j = 0; j < bmap.Height; j++)
                        {
                            System.Drawing.Color c = bmap.GetPixel(i, j);

                            var blue = c.B;
                            var red = c.R;
                            var green = c.G;

                            var saturation = c.GetSaturation();
                            var hue = c.GetHue();
                            var brightness = c.GetBrightness();
                        }
                    }

                }
            }

            if (painting.Description == null)
                painting.Description = "No available description";
            if (painting.CurrentOwner == null)
                painting.CurrentOwner = "N/A";

            db.Paintings.Add(painting);
            db.SaveChanges();

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", painting.ApplicationUserId);
            ViewBag.GalleryID = new SelectList(db.Galleries, "ID", "Name", painting.GalleryID);
            ViewBag.InfluenceID = new SelectList(db.Influences, "ID", "Name", painting.InfluenceID);
            ViewBag.PainterID = new SelectList(db.Painters, "ID", "Name", painting.PainterID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Name", painting.ProfileID);
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", painting.TypeID);

            return View(painting);
        }

        public List<double> ConvertXYZ_To_CIELAB(double x, double  y,double z)
        {
            var triple = new List<double> { x, y, z };
            var t0 = 0.008856;
            var a = 7.787;
            var b = 16 / 116;

            var refNumbers = new List<double> { 95.047, 100.0, 108.883 }; 
            foreach(var i in triple)
            {

            }

            for(int i = 0; i < 3; i++)
            {
                triple[i] /= refNumbers[i];
                var c = triple[i];

                if (c > t0)
                    triple[i] = Math.Pow(c, 1 / 3);
                else
                    triple[i] = a * c + b;

            }

            var l = 116 * triple[0] - 16;
            a = 500 * (triple[0] - triple[1]);
            b = (int)(200 * (triple[1] - triple[2]));
            return new List<double> { l, a, b };
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
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Name", painting.ProfileID);
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", painting.TypeID);
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,CurrentOwner,HarmonyIndex,ConstrastIndex,LuminosityIndex,Link,ApplicationUserId,PainterID,TypeID,InfluenceID,ProfileID,GalleryID")] Painting painting)
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
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Name", painting.ProfileID);
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
