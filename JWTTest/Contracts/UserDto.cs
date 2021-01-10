using System.ComponentModel.DataAnnotations;

namespace MemoApp.Contracts
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public UserType Type { get; set; }
    }

    public enum UserType
    {
        Admin = 1,
        User = 2
    }
}
