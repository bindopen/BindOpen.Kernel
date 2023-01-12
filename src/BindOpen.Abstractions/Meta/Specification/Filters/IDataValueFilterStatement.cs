using BindOpen.Meta.Items;
using System.Collections.Generic;

namespace BindOpen.Meta.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataValueFilterStatement : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDataValueFilter> Filters { get; set; }
    }
}