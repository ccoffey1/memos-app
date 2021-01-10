using System;
using System.Collections.Generic;

#nullable disable

namespace MemoApp.Models
{
    public partial class UserType
    {
        public UserType()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
