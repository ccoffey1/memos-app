#nullable disable

namespace JWTTest.Models
{
    public partial class UserLogin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
