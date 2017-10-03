using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.IO;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        ImageDb db = new ImageDb();
        public int PageSize = 4;
        public ActionResult Index(int page = 1)
        {
            ImageList model = new ImageList
            {
                Imageslist = db.Set<Image>().OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Set<Image>().Count(),
                }
            };
            return View(model);
        }
        public FileContentResult GetImage(int id)
        {
            var image = db.Set<Image>().FirstOrDefault(i => i.Id == id);
            return File(image.ImageData, "image/jpg");
        }
        public ActionResult Create()
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
                db.Image.Add(pic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pic);
        }
    }
}