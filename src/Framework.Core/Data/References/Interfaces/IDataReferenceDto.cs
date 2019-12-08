using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.References
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataReferenceDto : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        string DataHandlerUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSet PathDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElement SourceElement { get; set; }
    }
}