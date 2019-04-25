using System.Collections.Generic;

namespace BindOpen.Framework.Core.Application.Configuration
{
    public interface IUsableConfigurationDto : IConfiguration
    {
        List<string> UsingFilePaths { get; set; }
    }
}