using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Specification
{

    /// <summary>
    /// This class specifies the data value filter.
    /// </summary>
    [XmlType("DataValueFilter", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "value.filter", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataValueFilter : DataItem, IDataValueFilter
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The added values of this instance.
        /// </summary>
        /// <remarks>If empty then all the values are added.</remarks>
        [XmlElement("add")]
        public List<string> AddedValues { get; set; } = new List<string>();

        /// <summary>
        /// The removed values of this instance.
        /// </summary>
        /// <remarks>If empty then no value is removed.</remarks>
        [XmlElement("remove")]
        public List<string> RemovedValues { get; set; } = new List<string>();

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataValueFilter class.
        /// </summary>
        public DataValueFilter()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataValueFilter class specifying the action kind.
        /// </summary>
        /// <param name="addedValues">The allowed values to consider.</param>
        /// <param name="removedValues">The forbidden values to consider.</param>
        public DataValueFilter(
            List<string> addedValues = null,
            List<string> removedValues = null)
        {
            AddedValues = (addedValues ?? new List<string>());
            RemovedValues = (removedValues ?? new List<string>());
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accssors

        /// <summary>
        /// Gets the values allowed by this instance.
        /// </summary>
        /// <param name="allValues">All the values to consider.</param>
        /// <returns>Returns all the values allowed by this instance.</returns>
        public List<string> GetValues(List<string> allValues = null)
        {
            allValues = new List<string>((AddedValues == null || AddedValues.Count == 0 ? allValues : AddedValues));
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
        public bool IsValueAllowed(String value, List<string> allValues = null)
        {
            return GetValues(allValues).Contains(value);
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Repairs this instance.
        /// </summary>
        /// <param name="allValues">All the values to consider.</param>
        public void Repair(List<string> allValues = null)
        {
            AddedValues.RemoveAll(p => allValues.Any(q => p.KeyEquals(q)));
            AddedValues = AddedValues.Distinct().ToList();

            RemovedValues.RemoveAll(p => allValues.Any(q => p.KeyEquals(q)));
            RemovedValues = RemovedValues.Distinct().ToList();
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
            return new DataValueFilter()
            {
                AddedValues = new List<string>(AddedValues),
                RemovedValues = new List<string>(RemovedValues),
            };
        }

        #endregion
    }

}
