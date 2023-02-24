using BindOpen.Data.Conditions;
using BindOpen.Data.Configuration;
using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public abstract class BdoSpec : BdoItem, IBdoSpec
    {
        // --------------------------------------------------
        // CONSTANTS
        // --------------------------------------------------

        #region Constants

        /// <summary>
        /// Names of the attribute areas of the TBdoSpec class.
        /// </summary>
        public static readonly string[] __AreaNames = new[]
        {
            nameof(DataAreaKind.Design),
            nameof(DataAreaKind.Constraints),
            nameof(DataAreaKind.Properties),
            nameof(BdoMetaDataAreaKind.Element),
            nameof(DataAreaKind.Items)
        };

        #endregion

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private List<DataMode> _availableValueModes = null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoSpec class.
        /// </summary>
        protected BdoSpec() : base()
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
            var dataElementSpec = base.Clone<BdoSpec>(areas);

            dataElementSpec.WithAliases(Aliases?.ToArray());
            dataElementSpec.WithValueModes(ValueModes?.ToArray());
            dataElementSpec.WithConstraintStatement(ConstraintStatement.Clone<BdoConfigurationSet>());
            dataElementSpec.WithSpecificationLevels(SpecificationLevels?.ToArray());
            dataElementSpec.WithSubSpecs(SubSpecs?.ToArray());

            return dataElementSpec;
        }

        #endregion

        // --------------------------------------------------
        // IBdoSpec Implementation
        // --------------------------------------------------

        #region IBdoSpec

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

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

        // --------------------------------------------------
        // IBdoSpec Implementation
        // --------------------------------------------------

        #region IBdoSpec

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param key="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public virtual bool IsCompatibleWithData(
            object item)
        {
            if (item == null)
                return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICondition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="condition"></param>
        /// <returns></returns>
        public IBdoSpec WithCondition(ICondition condition)
        {
            Condition = condition;
            return this;
        }

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        public List<string> Aliases { get; set; }

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        public bool IsAllocatable { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isAllocatable"></param>
        /// <returns></returns>
        public IBdoSpec AsAllocatable(bool isAllocatable = true)
        {
            IsAllocatable = isAllocatable;

            return this;
        }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoExpression ItemExpression { get; set; }

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        public List<DataMode> ValueModes
        {
            get => _availableValueModes;
            set
            {
                if (value == null || value.Count == 0 || value.Contains(DataMode.Any))
                    _availableValueModes = new List<DataMode>() { DataMode.Any };
                else
                    _availableValueModes = value;
            }
        }

        /// <summary>
        /// The default item of this instance.
        /// </summary>
        public object DefaultItem { get; set; }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        public uint MinimumItemNumber { get; set; } = 0;

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        public uint? MaximumItemNumber { get; set; }

        /// <summary>
        /// Indicates whether the value of this instance is a list.
        /// </summary>
        public bool IsValueList => MaximumItemNumber == null || MaximumItemNumber > 1;

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public RequirementLevels ItemRequirementLevel
        {
            get
            {
                if (MaximumItemNumber == 0)
                {
                    return RequirementLevels.Forbidden;
                }
                else if (MinimumItemNumber > 0)
                {
                    return RequirementLevels.Required;
                }
                else if (MinimumItemNumber == 0)
                {
                    return RequirementLevels.Optional;
                }
                else
                {
                    return RequirementLevels.None;
                }
            }
        }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        public List<SpecificationLevels> ItemSpecificationLevels { get; set; }

        /// <summary>
        /// Data constraint statement of this instance.
        /// </summary>
        public IBdoConfigurationSet ConstraintStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IBdoSpec> SubSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public IBdoSpec GetSubSpec(string name)
        {
            return SubSpecs.FirstOrDefault(q => q.BdoKeyEquals(name));
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            ConstraintStatement?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
