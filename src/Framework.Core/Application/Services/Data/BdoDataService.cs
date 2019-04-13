using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Application.Services.Data
{
    /// <summary>
    /// This class represents a BindOpen data service.
    /// </summary>
    public class BdoDataService : DataItem, IBdoDataService
    {
        /// <summary>
        /// The connection of this instance.
        /// </summary>
        protected IConnection _connection = null;

        /// <summary>
        /// The connection of this instance.
        /// </summary>
        public IConnection Connection
        {
            get { return this._connection; }
        }

        /// <summary>
        /// Initializes a new instance of the DataService class.
        /// </summary>
        public BdoDataService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataService class.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        public BdoDataService(IConnection connection)
        {
            this._connection = connection;
        }
    }
}
