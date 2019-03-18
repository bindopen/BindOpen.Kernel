using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Specification.Filters
{

    /// <summary>
    /// This interface specifies the data value filter.
    /// </summary>
    [Serializable()]
    [XmlType("DataValueFilter", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "value.filter", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataValueFilter : DataItem
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<String> _AddedValues = new List<String>();
        private List<String> _RemovedValues = new List<String>();

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The added values of this instance.
        /// </summary>
        /// <remarks>If empty then all the values are added.</remarks>
        [XmlElement("add")]
        public List<String> AddedValues
        {
            get { return this._AddedValues; }
            set { this._AddedValues = value; }
        }

        /// <summary>
        /// Specification of the AddedValues property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean AddedValuesSpecified
        {
            get
            {
                return this.AddedValues != null && this.AddedValues.Count > 0;
            }
        }

        /// <summary>
        /// The removed values of this instance.
        /// </summary>
        /// <remarks>If empty then no value is removed.</remarks>
        [XmlElement("remove")]
        public List<String> RemovedValues
        {
            get { return this._RemovedValues; }
            set { this._RemovedValues = value; }
        }

        /// <summary>
        /// Specification of the RemovedValues property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean RemovedValuesSpecified
        {
            get
            {
                return this.RemovedValues != null && this.RemovedValues.Count > 0;
            }
        }

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
            List<String> addedValues = null,
            List<String> removedValues = null)
        {
            this._AddedValues = (addedValues ?? new List<String>());
            this._RemovedValues = (removedValues ?? new List<String>());
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
        public List<String> GetValues(List<String> allValues = null)
        {
            allValues = new List<String>((this._AddedValues==null || this._AddedValues.Count == 0 ? allValues : this._AddedValues));
            if (allValues != null && this._RemovedValues!=null)
                allValues.RemoveAll(p => this._RemovedValues.Contains(p));

            return allValues;
        }

        /// <summary>
        /// Indicates whether the value is allowed by this instance.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="allValues">All the values to consider.</param>
        /// <returns>Returns True if the specified is validated by this instance.</returns>
        public Boolean IsValueAllowed(String value, List<String> allValues = null)
        {
            return this.GetValues(allValues).Contains(value);
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
        public void Repair(List<String> allValues =null)
        {
            this._AddedValues.RemoveAll(p => allValues.Any(q => p.KeyEquals(q)));
            this._AddedValues = this._AddedValues.Distinct().ToList();

            this._RemovedValues.RemoveAll(p => allValues.Any(q => p.KeyEquals(q)));
            this._RemovedValues = this._RemovedValues.Distinct().ToList();
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
        public override Object Clone()
        {
            return new DataValueFilter()
            {
                AddedValues = new List<String>(this._AddedValues),
                RemovedValues = new List<String>(this._RemovedValues),
            };
        }

        #endregion

    }

}
