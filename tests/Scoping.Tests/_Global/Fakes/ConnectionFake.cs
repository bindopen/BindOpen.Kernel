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
/// This class represents a fake database connection used to test Pull and Push operations
/// within the BindOpen scoping environment.
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
    /// <param name="connector">The connector containing the configuration for this connection.</param>
    public ConnectionFake(ConnectorFake connector)
    {
        Connector = connector;
    }

    #endregion

    // ------------------------------------------
    // IBdoConnection METHODS
    // ------------------------------------------

    #region IBdoConnection_Methods

    // Open / Close -----------------------------

    /// <summary>
    /// Simulates connecting to a data source.
    /// </summary>
    /// <param name="log">The log to populate with execution details.</param>
    public override void Connect(IBdoLog log = null)
    {
        // In a real implementation, you would initialize your DB client here.
        log?.AddEvent(BdoEventKinds.Message, "Connection opened successfully.");
    }

    /// <summary>
    /// Simulates disconnecting from a data source.
    /// </summary>
    /// <param name="log">The log to populate with execution details.</param>
    public override void Disconnect(IBdoLog log = null)
    {
        // In a real implementation, you would dispose of your DB client here.
        log?.AddEvent(BdoEventKinds.Message, "Connection closed successfully.");
    }

    // Push / Pull -----------------------------

    /// <summary>
    /// Pulls a randomized list of fake entities from the "data source".
    /// </summary>
    /// <param name="paramSet">The parameters for the query (e.g., GroupId).</param>
    /// <param name="log">The execution log.</param>
    /// <returns>A collection of simulated entities.</returns>
    public IEnumerable<EntityFake> Pull(IBdoMetaSet paramSet = null, IBdoLog log = null)
    {
        var f = new Faker();

        // Simulate returning between 1 and 10 records
        var count = f.Random.Int(1, 10);
        var entities = Enumerable.Range(0, count)
            .Select(q =>
            {
                // Use the faker to generate a meta-object
                var meta = BdoEntityFaker.NewMetaObject()
                    .WithGroupId(paramSet?.GetData<string>(nameof(BdoSchema.GroupId)));

                // Instantiate the typed entity via the Scope
                return ScopingTestData.Scope.CreateEntity<EntityFake>(meta);
            });

        return entities;
    }

    /// <summary>
    /// Asynchronously pulls entities.
    /// </summary>
    public Task<IEnumerable<EntityFake>> PullAsync(IBdoMetaSet paramSet = null, IBdoLog log = null)
    {
        return Task.FromResult(Pull(paramSet, log));
    }

    /// <summary>
    /// Simulates pushing (Saving/Updating) entities to a data source.
    /// </summary>
    /// <param name="log">The execution log.</param>
    /// <param name="entities">The entities to push.</param>
    /// <returns>A collection of result items indicating the status of each push.</returns>
    public IEnumerable<IResultItem> Push(IBdoLog log = null, params EntityFake[] entities)
    {
        if (entities?.Any() == true)
        {
            foreach (var entity in entities)
            {
                // Simulate data persistence
                Debug.WriteLine($"Entity '{entity.Identifier}' pushed to the fake data source.");

                yield return BdoData.NewResultItem(ResourceStatus.Created, entity.Identifier);
            }
        }
    }

    /// <summary>
    /// Asynchronously pushes entities.
    /// </summary>
    public Task<IEnumerable<IResultItem>> PushAsync(IBdoLog log = null, params EntityFake[] entities)
    {
        return Task.FromResult(Push(log, entities));
    }

    #endregion
}