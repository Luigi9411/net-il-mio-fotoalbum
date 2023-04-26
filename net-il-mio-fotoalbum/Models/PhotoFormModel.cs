namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; } = new Photo { Url = "https://picsum.photos/200/300" };
        public IFormFile? ImageFormFile { get; set; }
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();

        public List<int> SelectedCategoryIds { get; set; } = new();

        public void SetImageFileFromFormFile()
        {
            if (ImageFormFile is null) return;

            using var stream = new MemoryStream();
            ImageFormFile!.CopyTo(stream);
            Photo.ImageFile = stream.ToArray();
        }
    }
}
