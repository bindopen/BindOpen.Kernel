using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping.Connectors;
using Bogus;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class represents a database connection.
/// </summary>
public class ConnectionFake : BdoConnection, ITBdoConnection<EntityFake>
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the ConnectionFake class.
    /// </summary>
    /// <param key="connector">The connector to consider.</param>
    public ConnectionFake(ConnectorFake connector)
    {
        Connector = connector;
    }

    #endregion

    // ------------------------------------------
    // IBdoConnection_Methods METHODS
    // ------------------------------------------

    #region IBdoConnection_Methods

    // Open / Close -----------------------------

    /// <summary>
    /// Connects this instance.
    /// </summary>
    /// <returns>Returns the log of process.</returns>
    public override void Connect(IBdoLog log = null)
    {
    }

    /// <summary>
    /// Disconnects this instance.
    /// </summary>
    /// <returns>Returns the log of process.</returns>
    public override void Disconnect(IBdoLog log = null)
    {
    }

    // Push / Pull -----------------------------

    public IEnumerable<EntityFake> Pull(IBdoMetaSet paramSet = null, IBdoLog log = null)
    {
        var f = new Faker();

        var count = f.Random.Int(1, 10);
        var entities = Enumerable.Range(0, count)
            .Select(q =>
            {
                var meta = BdoEntityFaker.NewMetaObject()
                    .WithGroupId(paramSet?.GetData<string>(nameof(BdoSchema.GroupId)));
                var entity = ScopingTestData.Scope.CreateEntity<EntityFake>(meta);
                return entity;
            });

        return entities;
    }

    public Task<IEnumerable<EntityFake>> PullAsync(IBdoMetaSet paramSet = null, IBdoLog log = null)
    {
        var entities = Pull(paramSet, log);

        return Task.FromResult(entities);
    }

    /// <summary>
    /// Pushes the specified entity objects.
    /// </summary>
    /// <param name="entities">The entity object to push.</param>
    /// <returns>Returns True whether the entities have been pushed.</returns>
    public IEnumerable<IResultItem> Push(IBdoLog log = null, params EntityFake[] entities)
    {
        if (entities?.Any() == true)
        {
            foreach (var entity in entities)
            {
                Debug.WriteLine(string.Format("Entity '{0}' pushed", entity.Identifier));
                yield return BdoData.NewResultItem(ResourceStatus.Created);
            }
        }
    }

    public Task<IEnumerable<IResultItem>> PushAsync(IBdoLog log = null, params EntityFake[] entities)
    {
        var results = Push(log, entities);

        return Task.FromResult(results);
    }

    #endregion
}
