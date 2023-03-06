using BindOpen.Data;
using BindOpen.Extensions;
using BindOpen.Extensions.Scripting;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordDefinition : IBdoFunctionDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes ParentValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoScriptwordDomainedDelegate RuntimeScopedFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoScriptwordDelegate RuntimeBasicFunction { get; set; }
    }
}