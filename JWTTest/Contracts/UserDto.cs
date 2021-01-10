namespace MemoApp.Contracts
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public UserType Type { get; set; }
    }

    public enum UserType
    {
        Admin = 1,
        User = 2
    }
}
