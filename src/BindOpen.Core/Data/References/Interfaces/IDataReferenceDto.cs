using BindOpen.Data.Elements;
using BindOpen.Data.Items;

namespace BindOpen.Data.References
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