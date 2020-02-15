using BindOpen.Data.Items;

namespace BindOpen.Application.Security
{
    /// <summary>
    /// This interface defines application credential.
    /// </summary>
    public interface IApplicationCredential : INamedDataItem
    {
        /// <summary>
        /// Domain ID.
        /// </summary>
        string DomainId { get; set; }

        /// <summary>
        /// Login.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Token value.
        /// </summary>
        string TokenValue { get; set; }
    }
}