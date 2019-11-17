using System;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Bots
{
    /// <summary>
    /// This static class proposes bot creators.
    /// </summary>
    public static class BotFactory
    {
        // Factories --------------------------

        /// <summary>
        /// Adds a BindOpen default bot service.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static TBot<DefaultBotSettings> CreateBindOpenDefaultBot(
            Action<ITBotOptions<DefaultBotSettings>> setupAction = null)
        {
            return CreateBindOpenBot<TBot<DefaultBotSettings>, DefaultBotSettings>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen bot service.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static TBot<T> CreateBindOpenBot<T>(
            Action<ITBotOptions<T>> setupAction = null)
            where T : class, IBotSettings, new()
        {
            return CreateBindOpenBot<TBot<T>, T>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen bot service.
        /// </summary>
        /// <typeparam name="THost">The class of bot to consider.</typeparam>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static THost CreateBindOpenBot<THost, T>(
            Action<ITBotOptions<T>> setupAction = null)
            where THost : TBot<T>, new()
            where T : class, IBotSettings, new()
        {
            THost host = new THost();
            host.Configure(setupAction);
            host.Start();
            return host;
        }
   }
}