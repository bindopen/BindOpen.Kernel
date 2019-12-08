using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Extensions;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
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