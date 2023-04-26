using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Aggiungi il titolo")]
        [StringLength(150, ErrorMessage = "Il titolo può contenere 150 caratteri")]
        public string Title { get; set; } = string.Empty;

        public IEnumerable<Photo>? Photos { get; set; }
    }
}
