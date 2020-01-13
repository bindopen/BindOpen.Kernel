using System.Collections.Generic;
using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Data.Specification
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