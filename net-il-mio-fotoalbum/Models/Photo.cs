using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Aggiungi il titolo")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Aggiungi la descrizione")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Deve contenere almeno cinque parole")]
        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public byte[]? ImageFile { get; set; }

        public string ImgSrc => ImageFile is null
           ? Url
           : $"data:image/png;base64,{Convert.ToBase64String(ImageFile)}";


        public bool Visible { get; set; }

        public List<Category>? Categories { get; set; }
    }
}
