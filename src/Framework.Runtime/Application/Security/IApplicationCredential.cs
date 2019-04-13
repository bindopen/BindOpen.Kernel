using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Runtime.Application.Security
{
    public interface IApplicationCredential : INamedDataItem
    {
        string DomainId { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string TokenValue { get; set; }
    }
}