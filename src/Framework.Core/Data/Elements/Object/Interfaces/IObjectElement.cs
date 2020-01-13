using System.Collections.Generic;
using BindOpen.Framework.Data.Elements;

namespace BindOpen.Framework.Data.Elements
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