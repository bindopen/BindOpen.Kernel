using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Runtime.Application.Languages
{
    public interface IApplicationLanguage : IDescribedDataItem
    {
        string CultureName { get; set; }
        string UICultureName { get; set; }
    }
}