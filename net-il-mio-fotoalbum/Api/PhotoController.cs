using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly ILogger<PhotoController> _logger;
        private readonly AlbumContext _context;

        public PhotoController(AlbumContext context, ILogger<PhotoController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPhotos([FromQuery] string? title)
        {
            var photos = _context.Photos
                .Where(p => title == null || p.Title.ToLower().Contains(title.ToLower()))
                .ToList();

            return Ok(photos);
        }

        [HttpGet("{id}")]
        public IActionResult GetPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (photo is null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

		[HttpPost]
		public IActionResult CreateMessage(Message message)
		{
			_context.Messages.Add(message);
			_context.SaveChanges();

			return Ok(message);
		}


    }
}

