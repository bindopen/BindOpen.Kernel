using BindOpen.System.Logging;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    public partial class TBdoSet<T> : BdoObject, ITBdoSet<T>
        where T : IReferenced
    {
        public void Update(
            ITBdoSet<T> refSet,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            TBdoSetExtensions.Update(this, refSet, updateModes, areas, log);
        }
    }
}
