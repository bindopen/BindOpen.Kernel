using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Runtime.Application.Security;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.Application.Configuration
{
    /// <summary>
    /// This interface defines application configuration.
    /// </summary>
    public interface IBdoHostConfiguration : IBdoBaseConfiguration
    {
        /// <summary>
        /// Credentials.
        /// </summary>
        List<ApplicationCredential> Credentials { get; set; }

        /// <summary>
        /// Data sources.
        /// </summary>
        List<Datasource> Datasources { get; set; }
    }
}