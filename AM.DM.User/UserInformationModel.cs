using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DM.User
{
    public class UserInformationModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsArticleUser { get; set; }
        public string Organization { get; set; }
        public string Profession { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
    }
}
