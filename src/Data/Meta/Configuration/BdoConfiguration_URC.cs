using BindOpen.Logging;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    public partial class BdoConfiguration : BdoMetaSet, IBdoConfiguration
    {
        public override void Update(
            object item,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (item is IBdoConfiguration config)
            {
                base.Update(item, areas, updateModes, log);

                // we update the children

                if (config._Children?.Any() == true)
                {
                    _children ??= BdoData.NewItemSet<IBdoConfiguration>();

                    _children?.Update(config._Children, areas, updateModes, log);
                }
            }
            else if (item is ITBdoSet<IBdoMetaData> set)
            {
                ITBdoSetExtensions.Update(this, set, updateModes, areas, log);
            }
            else if (item is IBdoMetaData setItem)
            {
                ITBdoSetExtensions.Update(this, setItem, updateModes, areas, log);
            }
        }
    }
}
