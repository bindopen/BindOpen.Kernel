using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Scripting;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordConfiguration : ITBdoExtensionTitledItemConfiguration<IBdoScriptwordDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        BdoScriptwordConfiguration SubScriptword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds WordKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSet ParameterDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordKind"></param>
        IBdoScriptwordConfiguration WithWordKind(ScriptItemKinds wordKind);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptword"></param>
        IBdoScriptwordConfiguration WithSubScriptword(BdoScriptwordConfiguration scriptword);
    }
}