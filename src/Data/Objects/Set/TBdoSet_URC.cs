using BindOpen.Data;
using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    public partial class TBdoSet<T> : BdoObject, ITBdoSet<T>
        where T : IReferenced
    {
        public virtual void Update(
            object item,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (item is ITBdoSet<T> set)
            {
                TBdoSetExtensions.Update(this, set, updateModes, areas, log);
            }
            else if (item is T setItem)
            {
                TBdoSetExtensions.Update(this, setItem, updateModes, areas, log);
            }
        }
    }
}
