using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppExtensionItemDefinitionDto : IIndexedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        string ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsEditable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsIndexed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string LibraryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFormat"></param>
        /// <param name="uiCulture"></param>
        /// <returns></returns>

        string GetText(LoggerFormat logFormat = LoggerFormat.Xml, string uiCulture = "*");
    }
}