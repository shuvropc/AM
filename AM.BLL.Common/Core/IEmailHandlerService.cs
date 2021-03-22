using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Common.Core
{
    public interface IEmailHandlerService
    {
        //https://github.com/mauricioaniche/repodriller
        public void SendEmail(string pToEmail, string pSubject, string pBody);

    }
}
