using JWTTest.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace JWTTest.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params UserType[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
