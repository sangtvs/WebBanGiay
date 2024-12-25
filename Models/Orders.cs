using System.ComponentModel.DataAnnotations;

namespace testt.Models
{
    public class Orders
    {
        [Key]  public int ID { get; set; }
        public int UserID { get; set; }
        public string OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string OrderStatus { get; set; }
    }
}
