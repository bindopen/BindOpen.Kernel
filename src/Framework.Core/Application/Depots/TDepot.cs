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
    public abstract class TDepot<T> : DataItemSet<T>, ITDepot<T> where T : IdentifiedDataItem
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TDepot class.
        /// </summary>
        public TDepot()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TDepot class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public TDepot(params T[] items) : base(items)
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
        public virtual ILog AddFromAllAssemblies()
        {
            return new Log();
        }

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        public virtual ILog AddFromAssembly(string assemblyName)
        {
            return new Log();
        }

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        public virtual ILog AddFromAssembly<T1>() where T1 : class
        {
            return new Log();
        }

        #endregion
    }
}
