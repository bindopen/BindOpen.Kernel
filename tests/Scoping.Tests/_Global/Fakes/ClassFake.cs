using System;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class represents a fake POCO used for testing reflection and metadata mapping.
/// </summary>
public class ClassFake
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the string property. Mapped to DataValueTypes.Text.
    /// </summary>
    public string StringProp { get; set; }

    /// <summary>
    /// Gets or sets the integer property. Mapped to DataValueTypes.Integer.
    /// </summary>
    public int IntProp { get; set; }

    /// <summary>
    /// Gets or sets the double property. Mapped to DataValueTypes.Number.
    /// </summary>
    public double DoubleProp { get; set; }

    /// <summary>
    /// Gets or sets the float property. Mapped to DataValueTypes.Number.
    /// </summary>
    public float FloatProp { get; set; }

    /// <summary>
    /// Gets or sets the date-time property. Mapped to DataValueTypes.Date.
    /// </summary>
    public DateTime DateTimeProp { get; set; }

    /// <summary>
    /// Gets or sets the time-span property. Mapped to DataValueTypes.Time.
    /// </summary>
    public TimeSpan TimeSpanProp { get; set; }

    #endregion
}