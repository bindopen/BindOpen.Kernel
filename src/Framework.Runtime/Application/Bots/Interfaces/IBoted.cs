namespace BindOpen.Framework.Runtime.Application.Bots
{
    /// <summary>
    /// The interface defines a boted item.
    /// </summary>
    public interface IBoted
    {
        // Execution ---------------------------------

        /// <summary>
        /// The bot.
        /// </summary>
        IBot Bot { get; set; }
    }
}