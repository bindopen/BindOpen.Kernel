using BindOpen.Framework.Core.Data.Depots;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Data.Stores
{
    /// <summary>
    /// This class represents a set of depots.
    /// </summary>
    public class BdoDataStore : DataItem, IBdoDataStore
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
        /// Adds a new depot of the specified depot class.
        /// </summary>
        /// <param name="action">The action to execute on the newly created depot.</param>
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

            return default(T);
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
        /// <param name="log"></param>
        public void LoadLazy(IBdoLog log)
        {
            foreach(var depotEntry in Depots)
            {
                var depot = depotEntry.Value;
                if (depot!=null)
                {
                    var subLog = log?.AddSubLog(title: "Loading depot '" + depot.Id + "'...", eventKind: EventKinds.Message);
                    depot.LoadLazy(subLog);

                    if (subLog.HasErrorsOrExceptions())
                    {
                        subLog.AddMessage("Could not load depot");
                    }
                }
            }
        }

        #endregion
    }
}
