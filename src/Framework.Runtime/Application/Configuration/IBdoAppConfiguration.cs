using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Runtime.Application.Security;

namespace BindOpen.Framework.Runtime.Application.Configuration
{
    public interface IBdoAppConfiguration : IConfiguration
    {
        List<ApplicationCredential> Credentials { get; set; }

        List<DataSource> DataSources { get; set; }
    }
}