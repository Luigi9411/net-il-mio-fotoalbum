using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
	public class Message
	{
			public int Id { get; set; }

			[Required(ErrorMessage = "inserisci un email.")]
			public string Email { get; set; } = string.Empty;

			[Required(ErrorMessage = "inserisci un messaggio.")]
			public string Text { get; set; } = string.Empty;
		}

}
