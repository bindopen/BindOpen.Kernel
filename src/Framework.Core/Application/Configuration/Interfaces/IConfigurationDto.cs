using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Application.Configuration
{
    public interface IConfigurationDto : IDataElementSet, INamed, IIdentifiedDataItem, ISavable
    {
        string CreationDate { get; set; }
        string CurrentFilePath { get; set; }
        string LastModificationDate { get; set; }

        IConfigurationDto AddGroup(string groupId, params IDataElement[] items);
    }
}