using BindOpen.Framework.Core.Extensions.Runtime.Items;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordDefinition : ITBdoExtensionItemDefinition<IBdoScriptwordDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoScriptwordDefinition Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, IBdoScriptwordDefinition> Children { get; }

        /// <summary>
        /// 
        /// </summary>
        BdoScriptwordFunction RuntimeFunction { get; set; }
    }
}