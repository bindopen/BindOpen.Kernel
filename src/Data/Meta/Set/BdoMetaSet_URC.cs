using BindOpen.System.Logging;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public partial class BdoMetaSet :
        TBdoSet<IBdoMetaData>,
        IBdoMetaSet
    {
        public virtual void Update(
            IBdoMetaData refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
        }
    }
}
