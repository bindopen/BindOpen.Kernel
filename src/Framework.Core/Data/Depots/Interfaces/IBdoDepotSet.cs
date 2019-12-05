using BindOpen.Framework.Core.Data.Items;
using System.Collections;

namespace BindOpen.Framework.Core.Data.Depots
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDepotSet : IDataItem
    {
        /// <summary>
        /// The depots of this instance.
        /// </summary>
        Hashtable Depots { get; }

        /// <summary>
        /// Adds the specified depot.
        /// </summary>
        /// <param name="depot">The depot to add.</param>
        void Add(IBdoDepot depot);

        /// <summary>
        /// Gets the depot of the specified type.
        /// </summary>
        T Get<T>() where T : IBdoDepot;

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}