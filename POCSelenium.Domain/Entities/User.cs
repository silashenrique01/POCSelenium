using POCSelenium.Domain.Entities;
using System.Collections.Generic;

namespace POCSelenium.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
    }

}