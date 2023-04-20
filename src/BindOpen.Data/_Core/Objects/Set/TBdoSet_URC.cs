using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    public partial class TBdoSet<T> : BdoObject, ITBdoSet<T>
        where T : IReferenced
    {
        public void Update(
            ITBdoSet<T> refSet,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoBaseLog log = null)
        {
            TBdoSetExtensions.Update(this, refSet, updateModes, areas, log);
        }
    }
}
