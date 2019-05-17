using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This class represents the named item result.
    /// </summary>
    public class NamedItemResult
    {
        public string Name { get; set; }

        public ResourceStatus Status { get; set; }

        public NamedItemResult(string name, ResourceStatus status)
        {
            Name = name;
            Status = status;
        }
    }
}