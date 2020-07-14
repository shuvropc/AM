using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Common.Core
{
    public interface IEmailHandlerService
    {
        public void SendEmail(string pToEmail, string pSubject, string pBody);

    }
}
