using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Tests;

/// <summary>
/// This class represents a fake class.
/// </summary>
public class WrapperClassFake
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// The string value of this instance.
    /// </summary>
    [BdoProperty(Name = "stringValue")]
    public string StringValue { get; set; }

    /// <summary>
    /// The integer value of this instance.
    /// </summary>
    [BdoProperty(Name = "intValue")]
    public int IntValue { get; set; }

    /// <summary>
    /// Enumeration value of this instance.
    /// </summary>
    [BdoProperty(Name = "enumValue")]
    public AccessibilityLevels EnumValue { get; set; }

    #endregion
}