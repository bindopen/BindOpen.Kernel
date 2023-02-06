using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This abstract class represents a data specification.
    /// </summary>
    public abstract class DataSpecification : BdoItem, IDataSpecification
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new insance of the DataSpecification class.
        /// </summary>
        protected DataSpecification()
        {
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
            var specification = base.Clone<DataSpecification>(areas);
            return specification;
        }

        #endregion

        // --------------------------------------------------
        // IDataSpecification Implementation
        // --------------------------------------------------

        #region IDataSpecification

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param name="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public virtual bool IsCompatibleWithItem(object item)
        {
            if (item == null)
                return true;

            return true;
        }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueTypes DataValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        public RequirementLevels RequirementLevel { get; set; } = RequirementLevels.None;

        /// <summary>
        /// The requirement script of this instance.
        /// </summary>
        public IBdoExpression RequirementExpression { get; set; }

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        public InheritanceLevels InheritanceLevel { get; set; } = InheritanceLevels.None;

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        public List<SpecificationLevels> SpecificationLevels { get; set; }

        /// <summary>
        /// Level of accessibility of this instance.
        /// </summary>
        public AccessibilityLevels AccessibilityLevel { get; set; } = AccessibilityLevels.Public;

        #endregion

        // ------------------------------------------
        // IIndexed Implementation
        // ------------------------------------------

        #region IIndexed

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public string Key() => Name;

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        #endregion
    }
}
