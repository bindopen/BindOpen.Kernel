namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// This enumeration lists all the possible application log kinds.
    /// </summary>
    public enum ApplicationLogKind
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Application logs.
        /// </summary>
        Application,

        /// <summary>
        /// Runtime logs.
        /// </summary>
        Runtime,

        /// <summary>
        /// Runtime users logs.
        /// </summary>
        Runtime_Users,

        /// <summary>
        /// Anonymous runtime users logs.
        /// </summary>
        Runtime_Users_Anonymous
    }
}