using System;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a decorative attribute used to identify and configure 
/// BindOpen connectors via reflection.
/// </summary>
/// <remarks>
/// Apply this attribute to classes implementing <see cref="IBdoConnector"/> to 
/// allow the discovery engine to automatically index them within the kernel.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class BdoConnectorAttribute : MetaExtensionAttribute
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoConnectorAttribute"/> class.
    /// </summary>
    public BdoConnectorAttribute() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoConnectorAttribute"/> class with a specified name.
    /// </summary>
    /// <param name="name">The unique name of the connector (e.g., "bindopen.datasources$mssql").</param>
    public BdoConnectorAttribute(string name) : base(name)
    {
    }

    #endregion
}