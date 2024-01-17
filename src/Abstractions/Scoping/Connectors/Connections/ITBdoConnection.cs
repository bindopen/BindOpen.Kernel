using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BindOpen.Scoping.Connectors
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface ITBdoConnection<TEntity> : IBdoConnection
        where TEntity : IBdoEntity
    {
        // Pull

        /// <summary>
        /// Pulls entity objects using the specified parameter set.
        /// </summary>
        /// <param name="paramSet">The set of meta parameters.</param>
        /// <returns>Returns the entity objects.</returns>
        IEnumerable<TEntity> Pull(IBdoMetaSet paramSet = null, IBdoLog log = null);

        /// <summary>
        /// Pulls entity objects using the specified parameter set.
        /// </summary>
        /// <param name="paramSet">The set of meta parameters.</param>
        /// <returns>Returns the entity objects.</returns>
        Task<IEnumerable<TEntity>> PullAsync(IBdoMetaSet paramSet = null, IBdoLog log = null);

        // Push

        /// <summary>
        /// Pushes the specified entity objects.
        /// </summary>
        /// <param name="entities">The entity object to push.</param>
        /// <returns>Returns True whether the entities have been pushed.</returns>
        IEnumerable<IResultItem> Push(IBdoLog log = null, params TEntity[] entities);

        /// <summary>
        /// Pushes the specified entity objects.
        /// </summary>
        /// <param name="entities">The entity object to push.</param>
        /// <returns>Returns True whether the entities have been pushed.</returns>
        Task<IEnumerable<IResultItem>> PushAsync(IBdoLog log = null, params TEntity[] entities);
    }
}
