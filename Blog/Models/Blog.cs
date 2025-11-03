using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

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

        public string ImageUrl1 { get; set; }

        public string ImageFileName1 { get; set; }

        public string ImageUrl2 { get; set; }

        public string ImageFileName2 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsPublished { get; set; } = true;
    }
}
