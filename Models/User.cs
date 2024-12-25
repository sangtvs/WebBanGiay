using System.ComponentModel.DataAnnotations;

namespace testt.Models
{
    public class User
    {
        [Key] public int ID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
    }
}
