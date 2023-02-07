using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Items
{

    /// <summary>
    /// This class specifies the data value filter.
    /// </summary>
    public class StringFilter : BdoItem, IStringFilter
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StringFilter class.
        /// </summary>
        public StringFilter()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the StringFilter class specifying the action kind.
        /// </summary>
        /// <param name="addedValues">The allowed values to consider.</param>
        /// <param name="removedValues">The forbidden values to consider.</param>
        public StringFilter(
            List<string> addedValues = null,
            List<string> removedValues = null)
        {
            AddedValues = addedValues ?? new List<string>();
            RemovedValues = removedValues ?? new List<string>();
        }

        #endregion

        // --------------------------------------------------
        // IStringFilter Implementation
        // --------------------------------------------------

        #region IStringFilter

        /// <summary>
        /// The added values of this instance.
        /// </summary>
        /// <remarks>If empty then all the values are added.</remarks>
        public List<string> AddedValues { get; set; }

        /// <summary>
        /// The removed values of this instance.
        /// </summary>
        /// <remarks>If empty then no value is removed.</remarks>
        public List<string> RemovedValues { get; set; }

        /// <summary>
        /// Gets the values allowed by this instance.
        /// </summary>
        /// <param name="allValues">All the values to consider.</param>
        /// <returns>Returns all the values allowed by this instance.</returns>
        public List<string> GetValues(List<string> allValues = null)
        {
            allValues = new List<string>(AddedValues == null || AddedValues.Count == 0 ? allValues : AddedValues);
            if (allValues != null && RemovedValues != null)
                allValues.RemoveAll(p => RemovedValues.Contains(p));

            return allValues;
        }

        /// <summary>
        /// Indicates whether the value is allowed by this instance.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="allValues">All the values to consider.</param>
        /// <returns>Returns True if the specified is validated by this instance.</returns>
        public bool IsValueAllowed(string value, List<string> allValues = null)
        {
            return GetValues(allValues).Contains(value);
        }

        /// <summary>
        /// Repairs this instance.
        /// </summary>
        /// <param name="allValues">All the values to consider.</param>
        public void UpdateWith(List<string> allValues = null)
        {
            AddedValues?.RemoveAll(p => allValues.Any(q => p.BdoKeyEquals(q)));
            AddedValues = AddedValues?.Distinct().ToList();

            RemovedValues?.RemoveAll(p => allValues.Any(q => p.BdoKeyEquals(q)));
            RemovedValues = RemovedValues?.Distinct().ToList();
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            return new StringFilter()
            {
                AddedValues = new List<string>(AddedValues),
                RemovedValues = new List<string>(RemovedValues),
            };
        }

        #endregion
    }

}
