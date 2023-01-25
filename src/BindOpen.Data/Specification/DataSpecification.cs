using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Linq;

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
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public IDataSpecification WithValueType(DataValueTypes valueType)
        {
            ValueType = valueType;

            return this;
        }

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        public RequirementLevels RequirementLevel { get; set; } = RequirementLevels.None;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public IDataSpecification WithRequirementLevel(RequirementLevels level)
        {
            RequirementLevel = level;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDataSpecification AsOptional()
            => WithRequirementLevel(RequirementLevels.Optional);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDataSpecification AsRequired()
            => WithRequirementLevel(RequirementLevels.Required);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDataSpecification AsForbidden()
            => WithRequirementLevel(RequirementLevels.Forbidden);

        /// <summary>
        /// The requirement script of this instance.
        /// </summary>
        public string RequirementScript { get; set; }

        public IDataSpecification WithRequirementScript(string script)
        {
            RequirementScript = script;

            return this;
        }

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        public InheritanceLevels InheritanceLevel { get; set; } = InheritanceLevels.None;

        public IDataSpecification WithInheritanceLevel(InheritanceLevels level)
        {
            InheritanceLevel = level;

            return this;
        }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        public List<SpecificationLevels> SpecificationLevels { get; set; }

        public IDataSpecification WithSpecificationLevels(params SpecificationLevels[] levels)
        {
            SpecificationLevels = levels.ToList();

            return this;
        }

        /// <summary>
        /// Level of accessibility of this instance.
        /// </summary>
        public AccessibilityLevels AccessibilityLevel { get; set; } = AccessibilityLevels.Public;

        public IDataSpecification WithAccessibilityLevel(AccessibilityLevels level)
        {
            AccessibilityLevel = level;

            return this;
        }

        #endregion

        // ------------------------------------------
        // IIndexedPoco Implementation
        // ------------------------------------------

        #region IIndexedPoco

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDataSpecification WithIndex(int? index)
        {
            Index = index;
            return this;
        }

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
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDataSpecification WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion

        // ------------------------------------------
        // INamedPoco Implementation
        // ------------------------------------------

        #region INamedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDataSpecification WithName(string name)
        {
            Name = BdoData.NewName(name, "spec_");
            return this;
        }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        public IDataSpecification AddTitle(KeyValuePair<string, string> item)
        {
            Title ??= BdoData.NewDictionary();
            Title.Add(item);
            return this;
        }

        public IDataSpecification WithTitle(IBdoDictionary dico)
        {
            Title = dico;
            return this;
        }

        public string GetTitleText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star)
        {
            return Title?[key, defaultKey];
        }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        public IDataSpecification AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoData.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IDataSpecification WithDescription(IBdoDictionary dico)
        {
            Description = dico;
            return this;
        }

        public string GetDescriptionText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star)
        {
            return Description?[key, defaultKey];
        }

        #endregion
    }
}
