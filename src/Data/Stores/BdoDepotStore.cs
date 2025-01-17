﻿using BindOpen.Logging;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a set of depots.
    /// </summary>
    public class BdoDepotStore : BdoObject, IBdoDepotStore
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
        public BdoDepotStore()
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
        public IBdoDepotStore Add<T>(T depot, Action<T> action = null) where T : IBdoDepot
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
        /// <param key="log">The BindOpen log used for tracking.</param>
        public bool LoadLazy(IBdoScope scope, IBdoLog log = null)
        {
            bool loaded = true;

            foreach (var depotEntry in Depots)
            {
                var depot = depotEntry.Value;
                if (depot != null)
                {
                    depot.WithScope(scope);
                    var childLog = log?.InsertChild(BdoEventLevels.Information, "Loading depot '" + depot.Identifier + "'...");
                    loaded &= depot.LoadLazy(childLog);

                    if (childLog?.HasEvent(BdoEventLevels.Error, BdoEventLevels.Fatal) == true)
                    {
                        childLog.AddEvent(BdoEventLevels.Information, "Could not load depot");
                    }
                }
            }

            return loaded;
        }

        #endregion
    }
}
