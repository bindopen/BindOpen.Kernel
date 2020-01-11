using BindOpen.Framework.Data.Items;
using BindOpen.Framework.System.Diagnostics;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Data.Depots
{
    /// <summary>
    /// This class represents a depot.
    /// </summary>
    [Serializable()]
    [XmlType("Depot", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "depot", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class TBdoDepot<T> : DataItemSet<T>, ITBdoDepot<T> where T : IdentifiedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The initialization function.
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
        public TBdoDepot()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoDepot class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public TBdoDepot(params T[] items) : base(items)
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

        #endregion
    }
}
