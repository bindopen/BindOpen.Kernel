﻿using BindOpen.Scoping.Scopes;
using BindOpen.Scoping.Data.Assemblies;
using BindOpen.Logging;
using System;

namespace BindOpen.Scoping.Data.Stores
{
    /// <summary>
    /// This class represents a depot.
    /// </summary>
    public abstract class TBdoDepot<T> : TBdoSet<T>, ITBdoDepot<T>
        where T : IReferenced
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoDepot class.
        /// </summary>
        protected TBdoDepot()
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoScoped Implementation
        // ------------------------------------------

        #region IBdoScoped

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        public TBdoDepot<T> WithScope(IBdoScope scope)
        {
            Scope = scope;
            return this;
        }

        #endregion

        // ------------------------------------------
        // ITBdoDepot Implementation
        // ------------------------------------------

        #region ITBdoDepot

        /// <summary>
        /// The initialization function of this instance.
        /// </summary>
        public Func<IBdoDepot, IBdoLog, int> LazyLoadFunction { get; set; }

        /// <summary>
        /// Add the items from all the assemblies.
        /// </summary>
        /// <param key="log">The log to append.</param>
        public IBdoDepot AddFromAllAssemblies(IBdoLog log = null)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                AddFromAssembly(BdoData.Assembly(assembly), log);
            }

            return this;
        }

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param key="assemblyName">The name of the assembly.</param>
        /// <param key="log">The log to append.</param>
        public virtual IBdoDepot AddFromAssembly(
            IBdoAssemblyReference reference,
            IBdoLog log = null) => this;

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        /// <param key="log">The log to append.</param>
        public IBdoDepot AddFromAssembly<T1>(IBdoLog log = null) where T1 : class
            => AddFromAssembly(BdoData.AssemblyFrom<T1>(), log);

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param key="log">The log to append.</param>
        public void LoadLazy(IBdoLog log)
        {
            LazyLoadFunction?.Invoke(this, log);
        }

        #endregion
    }
}