using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
       

    }
}
