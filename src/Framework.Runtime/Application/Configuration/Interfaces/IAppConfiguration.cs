using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Runtime.Application.Security;

namespace BindOpen.Framework.Runtime.Application.Configuration
{
    /// <summary>
    /// This interface defines application configuration.
    /// </summary>
    public interface IAppConfiguration : IBaseConfiguration
    {
        /// <summary>
        /// Credentials.
        /// </summary>
        List<ApplicationCredential> Credentials { get; set; }

        /// <summary>
        /// Data sources.
        /// </summary>
        List<DataSource> DataSources { get; set; }
    }
}