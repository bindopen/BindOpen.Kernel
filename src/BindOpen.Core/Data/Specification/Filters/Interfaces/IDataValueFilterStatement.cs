using System.Collections.Generic;
using BindOpen.Data.Items;

namespace BindOpen.Data.Specification
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