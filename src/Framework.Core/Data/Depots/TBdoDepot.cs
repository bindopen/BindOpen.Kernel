using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Depots
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

        #region Constructors

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
        public virtual IBdoLog AddFromAllAssemblies() => new BdoLog();

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        public virtual IBdoLog AddFromAssembly(string assemblyName) => new BdoLog();

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        public virtual IBdoLog AddFromAssembly<T1>() where T1 : class
            => AddFromAssembly(typeof(T1).Assembly.FullName);

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
