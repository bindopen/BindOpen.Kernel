using BindOpen.Kernel.Logging;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract partial class BdoMetaData : BdoObject, IBdoMetaData
    {
        public virtual void Update(
            object item,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (item is IBdoMetaData meta)
            {
                BdoMetaDataExtensions.Update(this, meta, areas, updateModes, log);
            }
        }
    }
}
