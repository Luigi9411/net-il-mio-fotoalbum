
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Data;

namespace net_il_mio_fotoalbum.Controllers
{
        public class MessageController : Controller
        {

            private readonly AlbumContext _context;

            public MessageController(AlbumContext context)
            {
                _context = context;
            }

            public IActionResult Message()
            {
                var messages = _context.Messages!.ToArray();
                return View(messages);
            }
        }
}


