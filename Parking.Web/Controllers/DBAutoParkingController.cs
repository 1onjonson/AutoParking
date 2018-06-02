using Parking.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Parking.Web.Controllers
{
    public class DBAutoParkingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Autoparking.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoParkingDto dbParking = db.Autoparking.Find(id);
            if (dbParking == null)
            {
                return HttpNotFound();
            }
            return View(dbParking);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Filled,TimeOut,AutoName,AutoNumber,ParkingNumber,Price")] AutoParkingDto dbParking)
        {
            if (ModelState.IsValid)
            {
                db.Autoparking.Add(dbParking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbParking);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoParkingDto dbParking = db.Autoparking.Find(id);
            if (dbParking == null)
            {
                return HttpNotFound();
            }
            return View(dbParking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Filled,TimeOut,AutoName,AutoNumber,ParkingNumber,Price")] AutoParkingDto dbParking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dbParking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dbParking);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoParkingDto dbParking = db.Autoparking.Find(id);
            if (dbParking == null)
            {
                return HttpNotFound();
            }
            return View(dbParking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoParkingDto dbParking = db.Autoparking.Find(id);
            db.Autoparking.Remove(dbParking);
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