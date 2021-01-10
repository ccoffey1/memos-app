using System;
using System.Collections.Generic;

#nullable disable

namespace JWTTest.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Userlogins = new HashSet<UserLogin>();
        }

        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<UserLogin> Userlogins { get; set; }
    }
}
