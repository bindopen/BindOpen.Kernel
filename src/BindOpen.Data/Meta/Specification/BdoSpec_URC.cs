using BindOpen.Logging;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public partial class BdoSpec : BdoItem, IBdoSpec
    {
        public virtual void Update(
            IBdoSpec refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
        }
    }
}
