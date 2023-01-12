using BindOpen.Meta.Items;
using BindOpen.Runtime.Scopes;
using System;
using BindOpen.Logging;

namespace BindOpen.Meta.Stores
{
    /// <summary>
    /// This class represents a depot.
    /// </summary>
    public abstract class TBdoDepot<T> : TBdoItemSet<T>, ITBdoDepot<T>
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new IBdoDepot WithId(string id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// The initialization function of this instance.
        /// </summary>
        public Func<IBdoDepot, IBdoLog, int> LazyLoadFunction { get; set; }

        /// <summary>
        /// Add the items from all the assemblies.
        /// </summary>
        /// <param name="log">The log to append.</param>
        public IBdoDepot AddFromAllAssemblies(IBdoLog log = null)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                AddFromAssembly(assembly.FullName, log);
            }

            return this;
        }

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="log">The log to append.</param>
        public virtual IBdoDepot AddFromAssembly(string assemblyName, IBdoLog log = null) => this;

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        /// <param name="log">The log to append.</param>
        public IBdoDepot AddFromAssembly<T1>(IBdoLog log = null) where T1 : class
            => AddFromAssembly(typeof(T1).Assembly.FullName, log);

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param name="log">The log to append.</param>
        public void LoadLazy(IBdoLog log)
        {
            LazyLoadFunction?.Invoke(this, log);
        }

        #endregion
    }
}
