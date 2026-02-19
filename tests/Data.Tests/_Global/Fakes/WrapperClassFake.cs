using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Tests;

/// <summary>
/// Represents a mock entity class used to test attribute-based metadata mapping.
/// </summary>
/// <remarks>
/// This class is used in conjunction with <see cref="MetaWrapperFake"/> to validate 
/// nested object resolution and property synchronization within the BindOpen Kernel.
/// </remarks>
public class WrapperClassFake
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the string value mapped via the "stringValue" metadata property.
    /// </summary>
    [BdoProperty(Name = "stringValue")]
    public string StringValue { get; set; }

    /// <summary>
    /// Gets or sets the integer value mapped via the "intValue" metadata property.
    /// </summary>
    [BdoProperty(Name = "intValue")]
    public int IntValue { get; set; }

    /// <summary>
    /// Gets or sets the accessibility level enumeration value.
    /// </summary>
    /// <remarks>
    /// This property is often used as a target for reference-based mapping tests 
    /// (e.g., "entityFake/enumValue").
    /// </remarks>
    [BdoProperty(Name = "enumValue")]
    public AccessibilityLevels EnumValue { get; set; }

    #endregion
}