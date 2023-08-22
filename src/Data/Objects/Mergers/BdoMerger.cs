using BindOpen.System.Data.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class specifies the data value filter.
    /// </summary>
    public class BdoMerger : BdoObject, IBdoMerger
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StringSet class.
        /// </summary>
        public BdoMerger()
        {
        }

        #endregion

        // --------------------------------------------------
        // IStringSet Implementation
        // --------------------------------------------------

        #region IStringSet

        /// <summary>
        /// The added values of this instance.
        /// </summary>
        /// <remarks>If empty then all the values are added.</remarks>
        public IList<string> AddedValues { get; set; }

        /// <summary>
        /// The removed values of this instance.
        /// </summary>
        /// <remarks>If empty then no value is removed.</remarks>
        public IList<string> RemovedValues { get; set; }

        /// <summary>
        /// Gets the values allowed by this instance.
        /// </summary>
        /// <param key="allValues">All the values to consider.</param>
        /// <returns>Returns all the values allowed by this instance.</returns>
        public IList<string> ToList()
        {
            return new List<string>().Merge(this)?.ToList();
        }

        /// <summary>
        /// Gets the values allowed by this instance.
        /// </summary>
        /// <param key="allValues">All the values to consider.</param>
        /// <returns>Returns all the values allowed by this instance.</returns>
        public string[] ToArray()
        {
            return ToList().ToArray();
        }

        /// <summary>
        /// Indicates whether the value is allowed by this instance.
        /// </summary>
        /// <param key="value">The value to validate.</param>
        /// <param key="allValues">All the values to consider.</param>
        /// <returns>Returns True if the specified is validated by this instance.</returns>
        public bool Contains(string value)
        {
            return new List<string>().Contains(value, this);
        }

        /// <summary>
        /// Repairs this instance.
        /// </summary>
        /// <param key="allValues">All the values to consider.</param>
        public void UpdateFromAll(List<string> allValues = null)
        {
            AddedValues = AddedValues?.Where(p => !allValues.Any(q => p.BdoKeyEquals(q)))?.Distinct().ToList();

            RemovedValues = RemovedValues?.Where(p => !allValues.Any(q => p.BdoKeyEquals(q)))?.Distinct().ToList();
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
        public override object Clone()
        {
            var obj = new BdoMerger()
            {
                AddedValues = new List<string>(AddedValues),
                RemovedValues = new List<string>(RemovedValues),
            };

            return obj;
        }

        #endregion
    }

}
