using BindOpen.Extensions.Connectors;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents a database service.
    /// </summary>
    public abstract class BdoDbService : BdoConnectedService, IBdoDbService
    {
        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public new IBdoDbConnector Connector
        {
            get { return _connector as IBdoDbConnector; }
        }

        /// <summary>
        /// Initializes a new instance of the BdoDbService class.
        /// </summary>
        protected BdoDbService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoDbService class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        protected BdoDbService(IBdoConnector connector)
        {
            WithConnector(connector);
        }
    }
}
