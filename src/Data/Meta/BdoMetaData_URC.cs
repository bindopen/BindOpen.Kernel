using BindOpen.Kernel.Data;
using BindOpen.Kernel.Logging;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract partial class BdoMetaData : BdoObject, IBdoMetaData
    {
        public virtual void Update(
            IBdoMetaData refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            BdoMetaDataExtensions.Update(this, refItem, areas, updateModes, log);
        }
    }
}
