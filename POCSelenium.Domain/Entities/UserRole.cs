using Microsoft.AspNetCore.Identity;
using POCSelenium.Domain;
using POCSelenium.Domain.Entities;

namespace Intuitive.Domain.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}