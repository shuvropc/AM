using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DM.Common
{
    public class EmailConfigModel
    {
        public string Server { get; set; }
        public string FromAddress { get; set; }
        public string MailPassword { get; set; }
        public string Port { get; set; }
    }
}
