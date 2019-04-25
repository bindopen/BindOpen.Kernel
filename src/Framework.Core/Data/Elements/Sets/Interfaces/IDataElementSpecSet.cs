using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{
    public interface IDataElementSpecSet : IDataItemSet<DataElementSpec>
    {
        List<DataElementSpec> Items { get; set; }

        IDataElementSpec GetItem(string name);
    }
}