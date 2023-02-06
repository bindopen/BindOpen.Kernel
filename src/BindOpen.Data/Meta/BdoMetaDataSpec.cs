using BindOpen.Data.Conditions;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public abstract class BdoMetaSpec : DataSpecification, IBdoMetaSpec
    {
        // --------------------------------------------------
        // CONSTANTS
        // --------------------------------------------------

        #region Constants

        /// <summary>
        /// Names of the attribute areas of the TBdoElementSpec class.
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

        private List<DataItemizationMode> _availableItemizationModes = null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoElementSpec class.
        /// </summary>
        protected BdoMetaSpec() : base()
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
            var dataElementSpec = base.Clone<BdoMetaSpec>(areas);

            dataElementSpec.WithAliases(Aliases?.ToArray());
            dataElementSpec.WithItemizationModes(ItemizationModes?.ToArray());
            dataElementSpec.WithConstraintStatement(ConstraintStatement.Clone<DataConstraintStatement>());
            dataElementSpec.WithSpecificationLevels(SpecificationLevels?.ToArray());
            dataElementSpec.WithSubSpecs(SubSpecs?.ToArray());

            return dataElementSpec;
        }

        #endregion

        // --------------------------------------------------
        // IBdoElementSpec Implementation
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ICondition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IBdoMetaSpec WithCondition(ICondition condition)
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
        /// <param name="isAllocatable"></param>
        /// <returns></returns>
        public IBdoMetaSpec AsAllocatable(bool isAllocatable = true)
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
        public List<DataItemizationMode> ItemizationModes
        {
            get => _availableItemizationModes;
            set
            {
                if ((value == null) || (value.Count == 0) || (value.Contains(DataItemizationMode.Any)))
                    _availableItemizationModes = new List<DataItemizationMode>() { DataItemizationMode.Any };
                else
                    _availableItemizationModes = value;
            }
        }

        /// <summary>
        /// The default item of this instance.
        /// </summary>
        public object DefaultItem { get; set; }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        public int MinimumItemNumber { get; set; } = 1;

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        public int MaximumItemNumber { get; set; } = -1;

        /// <summary>
        /// Indicates whether the value of this instance is a list.
        /// </summary>
        public bool IsValueList => (MaximumItemNumber == -1) || (MaximumItemNumber > 1);

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
                else if (MinimumItemNumber <= 0)
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
        public IDataConstraintStatement ConstraintStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IBdoMetaSpec> SubSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoMetaSpec GetSubSpec(string name)
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
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
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
