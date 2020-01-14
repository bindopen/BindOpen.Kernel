using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Application.Repositories
{
    /// <summary>
    /// This class represents a repository.
    /// </summary>
    public abstract class BdoDbRepository : BdoRepository, IBdoDbRepository
    {
        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public new IBdoDbConnector Connector
        {
            get { return _connector as IBdoDbConnector; }
        }

        /// <summary>
        /// Initializes a new instance of the BdoDbRepository class.
        /// </summary>
        protected BdoDbRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoDbRepository class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        protected BdoDbRepository(IBdoConnector connector)
        {
            SetConnector(connector);
        }
    }
}
