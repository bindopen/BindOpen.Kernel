using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BindOpen.Scoping.Connectors;

/// <summary>
/// Defines a strongly-typed connection specialized in exchanging entities of a specific type.
/// </summary>
/// <typeparam name="TEntity">The type of entity this connection handles.</typeparam>
public interface ITBdoConnection<TEntity> : IBdoConnection
    where TEntity : IBdoEntity
{
    // Pull -------------------------------------

    /// <summary>
    /// Pulls entities from the external source using the specified parameter set.
    /// </summary>
    /// <param name="paramSet">The set of parameters or filters to apply to the pull operation.</param>
    /// <param name="log">The log to use for tracking the operation.</param>
    /// <returns>Returns a collection of discovered entities.</returns>
    IEnumerable<TEntity> Pull(IBdoMetaSet paramSet = null, IBdoLog log = null);

    /// <summary>
    /// Asynchronously pulls entities from the external source using the specified parameter set.
    /// </summary>
    /// <param name="paramSet">The set of parameters or filters to apply to the pull operation.</param>
    /// <param name="log">The log to use for tracking the operation.</param>
    /// <returns>A task representing the asynchronous operation, returning a collection of entities.</returns>
    Task<IEnumerable<TEntity>> PullAsync(IBdoMetaSet paramSet = null, IBdoLog log = null);

    // Push -------------------------------------

    /// <summary>
    /// Pushes the specified entities to the external source.
    /// </summary>
    /// <param name="log">The log to use for tracking the operation.</param>
    /// <param name="entities">The entity instances to push.</param>
    /// <returns>Returns a collection of result items indicating the success or failure of each entity push.</returns>
    IEnumerable<IResultItem> Push(IBdoLog log = null, params TEntity[] entities);

    /// <summary>
    /// Asynchronously pushes the specified entities to the external source.
    /// </summary>
    /// <param name="log">The log to use for tracking the operation.</param>
    /// <param name="entities">The entity instances to push.</param>
    /// <returns>A task representing the asynchronous operation, returning result items for each entity.</returns>
    Task<IEnumerable<IResultItem>> PushAsync(IBdoLog log = null, params TEntity[] entities);
}