using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.References
{
    public interface IDataReferenceDto : IDataItem
    {
        string DataHandlerUniqueName { get; set; }
        DataElementSet PathDetail { get; set; }
        DataElement SourceElement { get; set; }
    }
}