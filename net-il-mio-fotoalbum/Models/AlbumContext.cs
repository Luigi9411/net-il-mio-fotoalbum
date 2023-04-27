using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


namespace net_il_mio_fotoalbum.Models
{
    public class AlbumContext : IdentityDbContext<IdentityUser>
    {
        public AlbumContext(DbContextOptions<AlbumContext> options) : base(options) { }
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Category> Categories { get; set; }

		public DbSet<Message> Messages { get; set; }

		public void Seed()
        {
            var photoSeed = new Photo[]
            {
                new()
                {
                    Url = "/img/bella.jpg",
                    Title =  "Entusiasmante",
                    Description = "Uno tra i migliori scatti",
                    Visible = true,
                },
                new()
                {
                    Url = "/img/chiesa.jpg",
                    Title =  "Incredibile",
                    Description = "Foto con un significato profondo per l'autore",
                    Visible = true,
                },
                new()
                {
                    Url = "/img/carica.jpg",
                    Title =  "Adrenalinica",
                    Description = "Foto per iniziare la giornata con quella carica in più",
                    Visible = true,
                }
            };

            if (!Photos.Any())
            {
                Photos.AddRange(photoSeed);
            }

            if (!Categories.Any())
            {
                var seed = new Category[]
                {
                    new()
                    {
                        Title = "Emozionante"
                    },
                    new()
                    {
                        Title = "Generale",
                        Photos = photoSeed
                    },
                    new()
                    {
                        Title = "Stimolante"
                    },
                    new()
                    {
                        Title = "Strazziante"
                    }
                };
                Categories.AddRange(seed);
            }

                if (!Roles.Any())
            {
                var seed = new IdentityRole[]
                {
                    new("Admin"),
                    new("User")
                };

                Roles.AddRange(seed);
            }

            if (Users.Any(u => u.Email == "admin@gmail.com" || u.Email == "user@gmail.com") && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@gmail.com");
                var user = Users.First(u => u.Email == "user@gmail.com");

                var adminRole = Roles.First(r => r.Name == "Admin");
                var userRole = Roles.First(r => r.Name == "User");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                    },
                    new()
                    {
                    UserId = user.Id,
                    RoleId = userRole.Id
                    },
                };

                UserRoles.AddRange(seed);
            }

            SaveChanges();
        }
    }
}