using System;
using System.Collections.Generic;

#nullable disable

namespace MemoApp.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            Memos = new HashSet<Memo>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual ICollection<Memo> Memos { get; set; }
    }
}
