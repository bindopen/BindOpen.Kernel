using System;

namespace BindOpen.Data.Meta;

/// <summary>
/// Represents the base attribute used to define metadata properties on C# elements.
/// It provides comprehensive options for naming, validation, and schema specification.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class BdoPropertyAttribute : Attribute, INamed, IGrouped, ITitled, IDescribed
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// The name of this property. Used for mapping against metadata keys.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The group ID of this instance. Useful for categorizing properties 
    /// (e.g., Input, Output, Config).
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    /// The display title of this property.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// A detailed description of what this property represents or how it is used.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The requirement level for items within this property.
    /// </summary>
    public RequirementLevels ItemRequirement { get; set; } = RequirementLevels.Any;

    /// <summary>
    /// A dynamic expression used to evaluate the item requirement level at runtime.
    /// </summary>
    public string ItemRequirementExp { get; set; }

    /// <summary>
    /// The requirement level for the property itself.
    /// </summary>
    public RequirementLevels Requirement { get; set; } = RequirementLevels.Any;

    /// <summary>
    /// A dynamic expression used to evaluate the property requirement level at runtime.
    /// </summary>
    public string RequirementExp { get; set; }

    /// <summary>
    /// The expected data value type for this property.
    /// </summary>
    public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

    /// <summary>
    /// Alternative keys used to map metadata to this property.
    /// </summary>
    public string[] Aliases { get; set; }

    /// <summary>
    /// The minimum number of data items allowed for this property.
    /// </summary>
    public uint? MinDataItemNumber { get; set; }

    /// <summary>
    /// The maximum number of data items allowed for this property.
    /// </summary>
    public uint? MaxDataItemNumber { get; set; }

    /// <summary>
    /// The schema specification levels applicable to this instance.
    /// </summary>
    public SpecificationLevels[] SpecLevels { get; set; }

    /// <summary>
    /// An optional reference path or expression for this property.
    /// </summary>
    public string Reference { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the BdoPropertyAttribute class.
    /// </summary>
    public BdoPropertyAttribute() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the BdoPropertyAttribute class with a name and aliases.
    /// </summary>
    /// <param name="name">The primary name.</param>
    /// <param name="aliases">Optional aliases.</param>
    public BdoPropertyAttribute(string name, params string[] aliases)
    {
        Name = name;
        Aliases = aliases;
    }

    /// <summary>
    /// Initializes a new instance of the BdoPropertyAttribute class with type and requirement rules.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="valueType">The expected value type.</param>
    /// <param name="requirement">The requirement level.</param>
    public BdoPropertyAttribute(
        string name,
        DataValueTypes valueType,
        RequirementLevels requirement = RequirementLevels.Any)
    {
        Name = name;
        ValueType = valueType;
        Requirement = requirement;
    }

    /// <summary>
    /// Initializes a new instance of the BdoPropertyAttribute class with dynamic item requirements.
    /// </summary>
    /// <param name="dataRequirement">The requirement level.</param>
    /// <param name="dataRequirementExp">The requirement expression.</param>
    public BdoPropertyAttribute(RequirementLevels dataRequirement, string dataRequirementExp)
    {
        ItemRequirement = dataRequirement;
        ItemRequirementExp = dataRequirementExp;
    }

    /// <summary>
    /// Initializes a new instance of the BdoPropertyAttribute class with cardinality constraints.
    /// </summary>
    /// <param name="min">Minimum number of items.</param>
    /// <param name="max">Maximum number of items.</param>
    public BdoPropertyAttribute(uint min, uint max)
    {
        MinDataItemNumber = min;
        MaxDataItemNumber = max;
    }

    /// <summary>
    /// Initializes a new instance of the BdoPropertyAttribute class with specification levels.
    /// </summary>
    /// <param name="levels">The specification levels.</param>
    public BdoPropertyAttribute(params SpecificationLevels[] levels)
    {
        SpecLevels = levels;
    }

    #endregion
}