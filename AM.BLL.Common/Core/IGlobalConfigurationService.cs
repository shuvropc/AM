using AM.DM.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Common.Core
{
    public interface IGlobalConfigurationService
    {
        public GlobalConfigModel GetMyConfiguration();
    }
}
