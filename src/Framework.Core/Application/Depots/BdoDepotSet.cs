using BindOpen.Framework.Core.Data.Items;
using System.Collections;

namespace BindOpen.Framework.Core.Application.Depots
{
    /// <summary>
    /// This class represents a set of depots.
    /// </summary>
    public class BdoDepotSet : DataItem, IBdoDepotSet
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The set of depots of this instance.
        /// </summary>
        public Hashtable Depots { get; } = new Hashtable();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDepotSet class.
        /// </summary>
        public BdoDepotSet()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified depot.
        /// </summary>
        /// <param name="depot">The depot to add.</param>
        public void Add(IBdoDepot depot)
        {
            Depots.Add(depot.GetType(), depot);
        }

        /// <summary>
        /// Gets the depot of the specified type.
        /// </summary>
        public T Get<T>() where T : IBdoDepot
        {
            if (Depots.ContainsKey(typeof(T)))
            {
                return (T)Depots[typeof(T)];
            }

            return default(T);
        }

        #endregion
    }
}
