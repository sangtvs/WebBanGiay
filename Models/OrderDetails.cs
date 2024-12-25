using System.ComponentModel.DataAnnotations;

namespace testt.Models
{
    public class OrderDetails
    {
        [Key] public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string ProductName { get; set; }
        public int Size { get; set; }
    }
}
