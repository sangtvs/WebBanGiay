using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace testt.Models
{
    public class Products
    {
        [Key] public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int QuantityInStock { get; set; }
        public string Brand { get; set; }
        public string ProductType { get; set; }
        public byte[] Image { get; set; }
        public IFormFile AnhGiay { get; set; }
    }

}
