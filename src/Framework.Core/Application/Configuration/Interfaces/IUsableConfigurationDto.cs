using System.Collections.Generic;

namespace BindOpen.Framework.Core.Application.Configuration
{
    public interface IUsableConfigurationDto : IBaseConfiguration
    {
        List<string> UsingFilePaths { get; set; }
    }
}