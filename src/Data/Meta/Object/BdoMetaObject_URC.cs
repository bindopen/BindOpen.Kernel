using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Logging;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public partial class BdoMetaObject : BdoMetaData,
        IBdoMetaObject
    {
        public void Update(
            ITBdoSet<IBdoMetaData> refSet,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            TBdoSetExtensions.Update(this, refSet, updateModes, areas, log);
        }
    }
}
