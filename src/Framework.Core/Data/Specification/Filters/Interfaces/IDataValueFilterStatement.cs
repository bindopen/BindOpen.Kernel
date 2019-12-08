using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataValueFilterStatement : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<DataValueFilter> Specifications { get; set; }
    }
}