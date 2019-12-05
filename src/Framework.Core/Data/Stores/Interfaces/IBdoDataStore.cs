using BindOpen.Framework.Core.Data.Depots;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Data.Stores
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
        /// Adds a new depot of the specified depot class.
        /// </summary>
        /// <param name="depot">The depot to add.</param>
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
        /// <param name="subLog">The log to append.</param>
        void LoadLazy(IBdoLog log);
    }
}