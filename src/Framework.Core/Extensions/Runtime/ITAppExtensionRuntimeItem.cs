using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Runtime
{
    /// <summary>
    /// This class represents an application extension runtime item.
    /// </summary>
    /// <typeparam name="T">The definition class of this instance.</typeparam>
    public interface ITAppExtensionRuntimeItem<T> where T : AppExtensionItemDefinition
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        IAppScope AppScope
        {
            get;
            set;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specififed configuration.
        /// </summary>
        /// <param name="configuration">The configuration to consider.</param>
        void SetConfiguration(TAppExtensionItemConfiguration<T> configuration);

        #endregion
    }
}

