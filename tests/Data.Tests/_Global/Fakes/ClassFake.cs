using System;

namespace BindOpen.Tests;

/// <summary>
/// Represents a mock POCO class used for testing property mapping and data type resolution.
/// </summary>
/// <remarks>
/// This class includes various standard .NET types to ensure the BindOpen Kernel 
/// correctly handles diverse data conversions during object-to-meta mapping.
/// </remarks>
public class ClassFake
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the string test property.
    /// </summary>
    public string StringProp { get; set; }

    /// <summary>
    /// Gets or sets the integer test property.
    /// </summary>
    public int IntProp { get; set; }

    /// <summary>
    /// Gets or sets the double-precision floating-point test property.
    /// </summary>
    public double DoubleProp { get; set; }

    /// <summary>
    /// Gets or sets the single-precision floating-point test property.
    /// </summary>
    public float FloatProp { get; set; }

    /// <summary>
    /// Gets or sets the date and time test property.
    /// </summary>
    public DateTime DateTimeProp { get; set; }

    /// <summary>
    /// Gets or sets the time interval test property.
    /// </summary>
    public TimeSpan TimeSpanProp { get; set; }

    #endregion
}