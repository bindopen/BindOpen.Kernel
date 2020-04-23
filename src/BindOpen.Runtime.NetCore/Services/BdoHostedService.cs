using BindOpen.Application.Scopes;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This service represents a BindOpen bot service that can be hosted in generic .Net core host.
    /// </summary>
    public class BdoHostedService : BdoJob, IBdoHostedService
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The BindOpen host of this instance.
        /// </summary>
        public IBdoHost Host { get => base._scope as IBdoHost; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Creates a new instance of the BdoHostedService class.
        /// </summary>
        public BdoHostedService() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the BdoHostedService class.
        /// </summary>
        /// <param name="host">The host to consider.</param>
        public BdoHostedService(IBdoHost host) : base()
        {
            WithScope(host);
        }

        #endregion

        // ------------------------------------------
        // METHODS
        // ------------------------------------------

        #region Methods

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Start();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Ends this instance.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            End();

            return Task.CompletedTask;
        }

        #endregion
    }
}
