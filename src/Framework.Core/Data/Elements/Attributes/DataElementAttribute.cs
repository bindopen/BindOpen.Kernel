﻿using System;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Attributes;
using BindOpen.Framework.Core.Data.Specification;

namespace BindOpen.Framework.Core.Data.Elements.Attributes
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class DataElementAttribute : DescribedDataItemAttribute
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private string[] _aliases = null;
        private DataAreaSpecification[] _areaSpecifications = null;

        private DataItemizationMode[] _availableItemizationModes = null;

        private string[] _defaultStringItems = null;

        private SpecificationLevels[] _itemSpecificationLevels = null;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of the group of this instance.
        /// </summary>
        public string GroupId { get; set; } = "";

        /// <summary>
        /// Specification of the GroupId property of this instance.
        /// </summary>
        public bool GroupIdSpecified => !string.IsNullOrEmpty(this.GroupId);

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueType ValueType { get; set; } = DataValueType.Any;

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        public string[] Aliases
        {
            get
            {
                return this._aliases;
            }
            set
            {
                this._aliases = value ?? new String[0];
            }
        }

        /// <summary>
        /// The area specifications of this instance.
        /// </summary>
        public DataAreaSpecification[] AreaSpecifications
        {
            get
            {
                return this._areaSpecifications;
            }
            set
            {
                this._areaSpecifications = value ?? new DataAreaSpecification[0];
            }
        }

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        public bool IsAllocatable { get; set; } = false;

        // Items ---------------------------------

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        public DataItemizationMode[] AvailableItemizationModes
        {
            get
            {
                return this._availableItemizationModes;
            }
            set
            {
                this._availableItemizationModes = value ?? new DataItemizationMode[0];
            }
        }

        /// <summary>
        /// Default string items of this instance.
        /// </summary>
        public string[] DefaultStringItems
        {
            get
            {
                return this._defaultStringItems;
            }
            set { this._defaultStringItems = value ?? new String[0]; }
        }

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
        public bool IsValueList
        {
            get
            {
                return (this.MaximumItemNumber == -1) | (this.MaximumItemNumber > 1);
            }
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public RequirementLevel ItemRequirementLevel
        {
            get
            {
                RequirementLevel itemRequirementLevel = RequirementLevel.None;
                if (this.MaximumItemNumber == 0)
                {
                    itemRequirementLevel = RequirementLevel.Forbidden;
                }
                else if (this.MinimumItemNumber > 0)
                    itemRequirementLevel = RequirementLevel.Required;
                else if (this.MinimumItemNumber <= 0)
                    itemRequirementLevel = RequirementLevel.Optional;
                else
                    itemRequirementLevel = RequirementLevel.None;
                return itemRequirementLevel;
            }
        }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        public SpecificationLevels[] ItemSpecificationLevels
        {
            get
            {
                return this._itemSpecificationLevels;
            }
            set { this._itemSpecificationLevels = value ?? new SpecificationLevels[0]; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataElementAttribute class.
        /// </summary>
        public DataElementAttribute() : base()
        {
        }

        #endregion
    }
}