using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BlogCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string ContentText1 { get; set; }

        [Required]
        public string ContentText2 { get; set; }

        [Required]
        public string ContentText3 { get; set; }

        [Required]
        public string ContentText4 { get; set; }

        [Required]
        public string ContentText5 { get; set; }

        public IFormFile ImageFile1 { get; set; }

        public IFormFile ImageFile2 { get; set; }

        public bool IsPublished { get; set; } = true;
    }
}
