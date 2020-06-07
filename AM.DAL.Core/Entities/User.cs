using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DAL.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
    }
}
