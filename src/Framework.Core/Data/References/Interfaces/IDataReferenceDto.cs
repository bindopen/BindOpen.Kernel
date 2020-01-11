using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Data.References
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