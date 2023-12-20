using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Connectors;
using BindOpen.Scoping.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BindOpen.Kernel.Tests
{
    /// <summary>
    /// This class represents a database connection.
    /// </summary>
    public class ConnectionFake : BdoConnection
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

        /// <summary>
        /// Pulls entity objects using the specified parameter set.
        /// </summary>
        /// <typeparam name="T">The BindOpen entity class to consider.</typeparam>
        /// <param name="paramSet">The set of meta parameters.</param>
        /// <returns>Returns the entity objects.</returns>
        public override IEnumerable<T> Pull<T>(IBdoMetaSet paramSet = null)
        {
            var f = new Faker();

            var count = f.Random.Int(1, 10);
            var entities = Enumerable.Range(0, count)
                .Select(q =>
                {
                    var meta = BdoEntityFaker.NewMetaObject()
                        .WithGroupId(paramSet?.GetData<string>(nameof(BdoSpec.GroupId)));
                    var entity = SystemData.Scope.CreateEntity<EntityFake>(meta);
                    return entity;
                })
                .Cast<T>();

            return entities;
        }

        /// <summary>
        /// Pushes the specified entity objects.
        /// </summary>
        /// <param name="entities">The entity object to push.</param>
        /// <returns>Returns True whether the entities have been pushed.</returns>
        public override bool Push(params IBdoEntity[] entities)
        {
            if (entities?.Any()==true)
            {
                foreach (var entity in entities)
                {
                    Debug.WriteLine(string.Format("Entity '{0}' pushed", entity.Id));
                }
            }

            return true;
        }

        #endregion
    }
}
