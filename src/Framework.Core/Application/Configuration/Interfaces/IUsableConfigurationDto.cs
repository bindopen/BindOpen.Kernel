using System.Collections.Generic;

namespace BindOpen.Framework.Core.Application.Configuration
{
    public interface IUsableConfigurationDto : IConfigurationDto
    {
        List<string> UsingFilePaths { get; set; }
    }
}