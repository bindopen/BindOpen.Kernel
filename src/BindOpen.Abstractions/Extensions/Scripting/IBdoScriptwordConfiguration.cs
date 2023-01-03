using BindOpen.Extensions;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordConfiguration : ITBdoExtensionTitledItemConfiguration<IBdoScriptwordDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoScriptwordConfiguration SubScriptword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptword"></param>
        IBdoScriptwordConfiguration WithSubScriptword(IBdoScriptwordConfiguration scriptword);

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