using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.IO;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        ImageDb db = new ImageDb();

        public FileContentResult Index()
        {
            var image = db.Set<Image>().FirstOrDefault(i => i.Id == 1);
            return File(image.ImageData, image.ImageMimeType);
        }
        public ActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Image pic, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                pic.ImageData = imageData;
                pic.ImageMimeType = ".jpg";
                pic.Id = 1;
                db.Image.Add(pic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pic);
        }
    }
}