using Intuitive.Domain.Identity;
using System.Collections.Generic;

namespace POCSelenium.Domain.Entities
{
    public class Role
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
