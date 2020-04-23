using BindOpen.Data.Common;
using BindOpen.Data.Items;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionItemDefinition : IDataItem, IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoExtensionDefinition ExtensionDefinition { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary> 
        string UniqueId { get; }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="logFormat"></param>
        ///// <param name="uiCulture"></param>
        ///// <returns></returns>

        //string GetText(BdoLoggerFormat logFormat = BdoLoggerFormat.Xml, string uiCulture = "*");
    }
}