using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AM.DAL.Core.Entities
{
    [Table("Profession", Schema = "Common")]
    public class Profession
    {
        public long Id { get; set; }
        public string ProfessionName { get; set; }
    }
}
