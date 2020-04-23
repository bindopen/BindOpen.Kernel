using System.Collections.Generic;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoUsableConfigurationDto : IBdoBaseConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> UsingFilePaths { get; set; }
    }
}