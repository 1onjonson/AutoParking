using parking;
using Parking.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parking.Web.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Print(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var dto = AutoParkingHelper.LoadFromStream(file.InputStream);

                using (var db = new ApplicationDbContext())
                {
                    var row = new Models.AutoParkingDto
                    {
                        Price = dto.Price,
                        Filled = dto.Filled,
                        TimeOut=dto.TimeOut,
                        AutoName=dto.AutoName,
                        AutoNumber=dto.AutoNumber,
                        ParkingNumber=dto.ParkingNumber,
                    };
                    db.Autoparking.Add(row);
                    db.SaveChanges();
                }

                return View(dto);
            }

            return RedirectToAction("Index");
        }
    }
}