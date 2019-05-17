using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This class represents the indentified item result.
    /// </summary>
    public class IdentifiedItemResult
    {
        public string Id { get; set; }

        public ResourceStatus Status { get; set; }

        public IdentifiedItemResult(string id, ResourceStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}