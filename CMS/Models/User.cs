using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // В реальном приложении хранить хэш!
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
