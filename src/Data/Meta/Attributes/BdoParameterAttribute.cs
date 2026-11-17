using System;

namespace BindOpen.Data.Meta;

/// <summary>
/// Represents an attribute used to define metadata requirements for method parameters.
/// This attribute allows the BindOpen engine to map and validate data when 
/// invoking methods dynamically.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public class BdoParameterAttribute : BdoPropertyAttribute
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the BdoParameterAttribute class.
    /// </summary>
    public BdoParameterAttribute() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the BdoParameterAttribute class with specified validation rules.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="valueType">The expected data value type.</param>
    /// <param name="requirement">The requirement level for this parameter.</param>
    public BdoParameterAttribute(
        string name,
        DataValueTypes valueType = DataValueTypes.Any,
        RequirementLevels requirement = RequirementLevels.Any)
        : base(name, valueType, requirement)
    {
    }

    /// <summary>
    /// Initializes a new instance of the BdoParameterAttribute class with dynamic requirement levels.
    /// </summary>
    /// <param name="dataRequirement">The requirement level.</param>
    /// <param name="dataRequirementExp">The expression defining the requirement logic.</param>
    public BdoParameterAttribute(
        RequirementLevels dataRequirement,
        string dataRequirementExp)
        : base(dataRequirement, dataRequirementExp)
    {
    }

    #endregion
}