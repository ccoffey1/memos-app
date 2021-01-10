using MemoApp.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace MemoApp.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params UserType[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
