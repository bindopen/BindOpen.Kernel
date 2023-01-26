using BindOpen.Data.Meta;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordConfiguration : ITBdoExtensionItemConfiguration<IBdoScriptwordDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoScriptwordConfiguration Add(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoScriptwordConfiguration WithItems(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds WordKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordKind"></param>
        IBdoScriptwordConfiguration WithWordKind(ScriptItemKinds wordKind);
    }
}