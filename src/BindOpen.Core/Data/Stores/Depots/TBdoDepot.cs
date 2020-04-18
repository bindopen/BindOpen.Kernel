using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a depot.
    /// </summary>
    [Serializable()]
    [XmlType("Depot", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "depot", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class TBdoDepot<T> : DataItemSet<T>, ITBdoDepot<T> where T : IIdentifiedDataItem
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IBdoScope _scope;

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoScope Scope => _scope;

        /// <summary>
        /// The initialization function of this instance.
        /// </summary>
        [XmlIgnore()]
        public Func<IBdoDepot, IBdoLog, int> LazyLoadFunction { get; set; }

        #endregion

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
        // MUTATORS
        // ------------------------------------------

        #region Mutators

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

        /// <summary>
        /// Sets the scope of this instance.
        /// </summary>
        /// <param name="scope">The scope to append.</param>
        public IBdoDepot WithScope(IBdoScope scope)
        {
            _scope = scope;

            return this;
        }

        #endregion
    }
}
