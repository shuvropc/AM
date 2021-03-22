using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Common.Core
{
    public interface IEmailHandlerService
    {
        //https://github.com/shuvropc/AM/edit/master/AM.BLL.Common/Core/IEmailHandlerService.cs
        public void SendEmail(string pToEmail, string pSubject, string pBody);

    }
}
