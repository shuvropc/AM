using AM.BLL.Common.Core;
using AM.DM.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Common.Infrastructure
{
    public class GlobalConfigurationService : IGlobalConfigurationService
    {
        private readonly IOptions<GlobalConfigModel> appSettings;
        public GlobalConfigurationService(IOptions<GlobalConfigModel> app)
        {
            appSettings = app;
        }

        public GlobalConfigModel GetMyConfiguration()
        {
            var result = appSettings.Value;
            return result;
        }

    }
}
