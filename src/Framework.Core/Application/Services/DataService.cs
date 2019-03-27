using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Application.Services
{
    /// <summary>
    /// This class represents a data service.
    /// </summary>
    public class DataService : DataItem, IDataService
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
        public DataService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataService class.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        public DataService(IConnection connection)
        {
            this._connection = connection;
        }
    }
}
