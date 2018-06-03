using Parking.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Web.Hosting;

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

        public ActionResult Download(int? id)
        {
            var ctx = new ApplicationDbContext();
            var g = ctx.Autoparking.Find(id);

            ExcelPackage pkg;
            using (var stream = System.IO.File.OpenRead(HostingEnvironment.ApplicationPhysicalPath + "template.xlsx"))
            {
                pkg = new ExcelPackage(stream);
                stream.Dispose();
            }

            var worksheet = pkg.Workbook.Worksheets[1];
            worksheet.Name = "Информация о парковке";

            worksheet.Cells[2, 3].Value = g.Filled.ToString();
            worksheet.Cells[3, 3].Value = g.TimeOut.ToString();
            worksheet.Cells[4, 3].Value = g.AutoName;
            worksheet.Cells[5, 3].Value = g.AutoNumber;
            worksheet.Cells[6, 3].Value = g.ParkingNumber;
            worksheet.Cells[8, 3].Value = g.Price;
            using (var cells = worksheet.Cells[2, 2, 6, 3])
            {
                cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            }
            using (var cells = worksheet.Cells[8, 2, 8, 3])
            {
                cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            }
            var ms = new MemoryStream();
            pkg.SaveAs(ms);

            return File(ms.ToArray(), "application/ooxml", ((g.FullName ?? "Без Названия") + g.Filled.ToString()).Replace(" ", "") + ".xlsx");
        }
    }
}