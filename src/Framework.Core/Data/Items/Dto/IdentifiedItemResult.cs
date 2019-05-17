using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This class represents the indentified single result.
    /// </summary>
    public class IdentifiedSingleResult
    {
        public string Id { get; set; }

        public ResourceStatus Status { get; set; }

        public IdentifiedSingleResult(string id, ResourceStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}