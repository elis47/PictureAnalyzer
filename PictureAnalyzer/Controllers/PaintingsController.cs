﻿using System;
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

namespace PictureAnalyzer.Controllers
{
    public class PaintingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paintings
        public ActionResult Index()
        {
            var paintings = db.Paintings.ToList();
                                    //.Include(p => p.ApplicationUser)
                                    //.Include(p => p.Gallery)
                                    //.Include(p => p.Influence)
                                    //.Include(p => p.Painter)
                                    //.Include(p => p.Profile)
                                    //.Include(p => p.Type)
                                    //.ToList();
            return View(paintings);
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
                    //string relativePath = "~/Images/" + Path.GetFileName(file.FileName);
                    //string physicalPath = Server.MapPath(relativePath);
                    string physicalPath = HttpContext.Request.PhysicalApplicationPath + "/Content/img/" + file.FileName;
                    file.SaveAs(physicalPath);

                    painting.Link = physicalPath;

                    /* Image processing part */

                    ImageProcessing i = new ImageProcessing(physicalPath);

                }
            }

            if (painting.Description == null)
                painting.Description = "No available description";
            if (painting.CurrentOwner == null)
                painting.CurrentOwner = "N/A";

            painting.ApplicationUserId = User.Identity.GetUserId();

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
