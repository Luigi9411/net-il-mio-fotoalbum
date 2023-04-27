using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;
        private readonly AlbumContext _context;

        public PhotoController(ILogger<PhotoController> logger, AlbumContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var photos = _context.Photos.ToArray();

            return View(photos);

        }

        public IActionResult ApiIndex()
        {
            return View();
        }


        public IActionResult Detail(int id)
        {
            var photo = _context.Photos.Include(p => p.Categories).SingleOrDefault(p => p.Id == id);

            if (photo is null)
           {
            return View("NotFound", "Photo not found.");
           }
           return View(photo);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
           var formModel = new PhotoFormModel
             {
              Categories = _context.Categories.ToArray(),
             };

          return View(formModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel form)
        {
           if (!ModelState.IsValid)
           {
              form.Categories = _context.Categories.ToArray();

             return View(form);
           }

           form.Photo.Categories = _context.Categories.Where(t => form.SelectedCategoryIds.Contains(t.Id)).ToList();

           form.SetImageFileFromFormFile();

              _context.Photos.Add(form.Photo);
              _context.SaveChanges();

           return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
           var photo = _context.Photos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

           if (photo is null)
           {
               return View("NotFound");
           }

           var formModel = new PhotoFormModel
           {
              Photo = photo,
              Categories = _context.Categories.ToArray(),
              SelectedCategoryIds = photo.Categories!.Select(t => t.Id).ToList()
           };

               return View(formModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PhotoFormModel form)
        {
            if (!ModelState.IsValid)
        {
            form.Categories = _context.Categories.ToArray();

               return View(form);
        }

             var savedPhoto = _context.Photos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

             if (savedPhoto is null)
        {
               return View("NotFound");
        }

             form.SetImageFileFromFormFile();

             savedPhoto.Title = form.Photo.Title;
             savedPhoto.Description = form.Photo.Description;
             savedPhoto.Url = form.Photo.Url;
             savedPhoto.ImageFile = form.Photo.ImageFile;
             savedPhoto.Categories = _context.Categories.Where(t => form.SelectedCategoryIds.Contains(t.Id)).ToList();

             _context.SaveChanges();

                return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
              var photoToDelete = _context.Photos.FirstOrDefault(p => p.Id == id);

              if (photoToDelete is null)
              {
                 return View("NotFound");
              }

             _context.Photos.Remove(photoToDelete);
             _context.SaveChanges();

                 return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
                    
        public IActionResult Error()
                
        {
                 return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
