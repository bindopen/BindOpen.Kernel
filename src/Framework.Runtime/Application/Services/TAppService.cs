using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Runtime.Application.Settings;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public abstract class TAppService<T> : AppService, ITAppService<T>
        where T : class, IBaseSettings, new()
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public new T Settings
        {
            get => base.Settings as T;
            set { base.Settings = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppService class.
        /// </summary>
        public TAppService(
            IAppHostScope appScope,
            T settings = default,
            ILogger[] loggers = null)
            : base(appScope, settings, loggers)
        {
        }

        #endregion

        // ------------------------------------------
        // LOAD MANAGEMENT
        // ------------------------------------------

        #region Load Management

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <returns>Returns the log of the task.</returns>
        protected override ILog Initialize<A>()
        {
            ILog log = base.Initialize<A>();

            if (GetType() == typeof(TAppService<T>))
                _isLoadCompleted = true;

            return log;
        }

        #endregion
    }
}