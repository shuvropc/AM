using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AM.DAL.Core.Entities
{
    [Table("Organization", Schema = "Common")]
    public class Organization
    {
        public long Id { get; set; }
        public string OrganizationName { get; set; }
    }
}
