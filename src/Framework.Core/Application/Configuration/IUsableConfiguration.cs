using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Core.Application.Configuration
{
    public interface IUsableConfiguration : IConfiguration
    {
        Configuration UsingConfiguration { get; set; }
        List<string> UsingFilePaths { get; set; }

        Configuration AddGroup(string groupId, params IDataElement[] items);
    }
}