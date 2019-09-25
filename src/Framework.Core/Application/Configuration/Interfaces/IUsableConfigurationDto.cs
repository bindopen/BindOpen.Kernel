using System.Collections.Generic;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUsableConfigurationDto : IBaseConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> UsingFilePaths { get; set; }
    }
}