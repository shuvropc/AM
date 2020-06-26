using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AM.DAL.Core.Entities
{
    [Table("UserInformation", Schema = "Security")]
    public class UserInformation : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsArticleUser { get; set; }
        public string Organization { get; set; }
        public string Profession { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
    }
}
