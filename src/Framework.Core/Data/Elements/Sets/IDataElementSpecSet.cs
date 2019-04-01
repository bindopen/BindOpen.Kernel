using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{
    public interface IDataElementSpecSet : IGenericDataItemSet<IDataElementSpec>
    {
        List<IDataElementSpec> Items { get; set; }

        IDataElementSpec GetItem(string name);
    }
}