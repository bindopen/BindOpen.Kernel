using BindOpen.System.Logging;

namespace BindOpen.System.Data.Meta
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
