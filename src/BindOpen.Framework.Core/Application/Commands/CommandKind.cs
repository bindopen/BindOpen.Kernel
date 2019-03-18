namespace BindOpen.Framework.Core.Application.Commands
{
    /// <summary>
    /// This enumeration represents the possible kinds of commands.
    /// </summary>
    public enum CommandKind
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Shell command.
        /// </summary>
        Shell,

        /// <summary>
        /// Command with reference.
        /// </summary>
        Reference,

        /// <summary>
        /// Command with script.
        /// </summary>
        Script
    }
}