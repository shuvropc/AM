using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DAL.Core.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
