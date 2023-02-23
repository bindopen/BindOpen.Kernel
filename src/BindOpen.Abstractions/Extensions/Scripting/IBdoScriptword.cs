using BindOpen.Data;
using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptword : IBdoHandledItem, IList<object>, INamed, IIdentified, IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword Child { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoScriptword Root(int levelMax = 50);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoScriptword Last(int levelMax = 50);
    }
}