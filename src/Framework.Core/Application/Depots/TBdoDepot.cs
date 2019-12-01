using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Application.Depots
{
    /// <summary>
    /// This class represents a depot.
    /// </summary>
    [Serializable()]
    [XmlType("Depot", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "depot", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class TBdoDepot<T> : DataItemSet<T>, IBdoTDepot<T> where T : IdentifiedDataItem
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TDepot class.
        /// </summary>
        public TBdoDepot()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TDepot class.
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
        public virtual IBdoLog AddFromAllAssemblies()
        {
            return new BdoLog();
        }

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        public virtual IBdoLog AddFromAssembly(string assemblyName)
        {
            return new BdoLog();
        }

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        public virtual IBdoLog AddFromAssembly<T1>() where T1 : class
        {
            return new BdoLog();
        }

        #endregion
    }
}
