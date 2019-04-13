using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Specification.Filters
{
    public interface IDataValueFilterStatement : IDataItem
    {
        List<DataValueFilter> Specifications { get; set; }
    }
}