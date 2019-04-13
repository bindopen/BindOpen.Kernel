using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Application.Configuration
{
    public interface ITConfiguration<T> : IDataItem, IReferenced where T : IConfigurationDto
    {
        T Dto { get; }
    }
}