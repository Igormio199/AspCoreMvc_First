using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        public string? Name { get; set; }

        [Range(1, 1000, ErrorMessage = "Цена от 1 до 1000")]
        public decimal Price { get; set; }
    }
}
