using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;

namespace BindOpen.Framework.Core.Data.Elements._Object
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectElement : IDataElement
    {
        /// <summary>
        /// 
        /// </summary>
        string ClassFullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DataElementSet> Objects { get; set; }

        /// <summary>
        /// 
        /// </summary>
        new ObjectElementSpec Specification { get; set; }
    }
}