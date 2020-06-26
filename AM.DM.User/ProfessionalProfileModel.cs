using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DM.User
{
    public class ProfessionalProfileModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public string ContactName { get; set; }

        public string ProfileDescription { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string CompanySize { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string StateProvince { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public DateTime? Birthday { get; set; }

        public string Gender { get; set; }
    }
}
