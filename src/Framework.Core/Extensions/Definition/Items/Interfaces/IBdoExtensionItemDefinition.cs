using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Definition;

namespace BindOpen.Framework.Extensions.Definition
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