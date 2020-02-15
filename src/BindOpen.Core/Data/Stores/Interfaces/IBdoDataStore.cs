using BindOpen.Data.Stores;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDataStore : IDataItem
    {
        /// <summary>
        /// The depots of this instance.
        /// </summary>
        Dictionary<Type, IBdoDepot> Depots { get; }

        /// <summary>
        /// Adds the specified depot executing the specified action.
        /// </summary>
        /// <param name="depot">The depot to consider.</param>
        /// <param name="action">The action to execute on the newly created depot.</param>
        /// <typeparam name="T">The depot class to consider.</typeparam>
        IBdoDataStore Add<T>(T depot, Action<T> action = null) where T : IBdoDepot;

        /// <summary>
        /// Gets the depot of the specified class.
        /// </summary>
        /// <typeparam name="T">The depot class to consider.</typeparam>
        T Get<T>() where T : IBdoDepot;

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Executes the lazy functions of all the depots of this instance.
        /// </summary>
        /// <param name="log">The log to append.</param>
        void LoadLazy(IBdoLog log);
    }
}