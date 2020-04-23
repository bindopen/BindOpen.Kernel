using BindOpen.Application.Configuration;
using BindOpen.Data.Items;
using BindOpen.Application.Security;
using System.Collections.Generic;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This interface defines application configuration.
    /// </summary>
    public interface IBdoAppConfiguration : IBdoBaseConfiguration
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