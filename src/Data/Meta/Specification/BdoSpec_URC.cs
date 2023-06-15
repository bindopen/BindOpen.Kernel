using BindOpen.System.Logging;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public partial class BdoSpec : BdoObject, IBdoSpec
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
