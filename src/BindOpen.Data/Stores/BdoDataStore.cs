using BindOpen.Logging;
using BindOpen.Scopes;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a set of depots.
    /// </summary>
    public class BdoDataStore : BdoObject, IBdoDataStore
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The set of depots of this instance.
        /// </summary>
        public Dictionary<Type, IBdoDepot> Depots { get; } = new Dictionary<Type, IBdoDepot>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDataStore class.
        /// </summary>
        public BdoDataStore()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified depot executing the specified action.
        /// </summary>
        /// <param key="depot">The depot to consider.</param>
        /// <param key="action">The action to execute on the newly created depot.</param>
        /// <typeparam name="T">The depot class to consider.</typeparam>
        public IBdoDataStore Add<T>(T depot, Action<T> action = null) where T : IBdoDepot
        {
            Depots.Add(typeof(T), depot);
            action?.Invoke(depot);

            return this;
        }

        /// <summary>
        /// Gets the depot of the specified type.
        /// </summary>
        public T Get<T>() where T : IBdoDepot
        {
            if (Depots.ContainsKey(typeof(T)))
            {
                return (T)Depots[typeof(T)];
            }

            return default;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            Depots?.Clear();
        }

        /// <summary>
        /// Executes the lazy functions of all the depots of this instance.
        /// </summary>
        /// <param key="scope">The scope to append.</param>
        /// <param key="log"></param>
        public void LoadLazy(IBdoScope scope, IBdoBaseLog log = null)
        {
            foreach (var depotEntry in Depots)
            {
                var depot = depotEntry.Value;
                if (depot != null)
                {
                    depot.WithScope(scope);
                    var childLog = log?.InsertChild(EventKinds.Message, "Loading depot '" + depot.Id + "'...");
                    depot.LoadLazy(childLog);

                    if (childLog?.HasEvent(EventKinds.Error, EventKinds.Exception) == true)
                    {
                        childLog.AddEvent(EventKinds.Message, "Could not load depot");
                    }
                }
            }
        }

        #endregion
    }
}
