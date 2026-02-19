using BindOpen.Data;
using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Tests;

/// <summary>
/// This class represents a fake meta wrapper used for testing configuration mapping.
/// </summary>
/// <remarks>
/// It inherits from <see cref="BdoConfigurationMetaWrapper"/> to test the framework's 
/// ability to sync properties between an object and a metadata set.
/// </remarks>
public class MetaWrapperFake : BdoConfigurationMetaWrapper
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the first test string property.
    /// </summary>
    [BdoProperty("test1")]
    public string Test1 { get; set; }

    /// <summary>
    /// Gets or sets the second test integer property.
    /// </summary>
    [BdoProperty("test2")]
    public int Test2 { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of integers for testing collection mapping.
    /// </summary>
    [BdoProperty("testDico")]
    public TBdoDictionary<int> Dico { get; set; }

    /// <summary>
    /// Gets or sets the list of strings for testing list mapping.
    /// </summary>
    [BdoProperty("testList")]
    public List<string> List { get; set; }

    /// <summary>
    /// Gets or sets the fake entity wrapper for testing nested object mapping.
    /// </summary>
    [BdoProperty("entityFake")]
    public WrapperClassFake EntityFake { get; set; }

    /// <summary>
    /// Gets or sets an enumeration value resolved via a relative reference.
    /// </summary>
    /// <remarks>
    /// This property tests the "Reference" capability of the BdoProperty attribute, 
    /// pointing to a sub-property of another entity.
    /// </remarks>
    [BdoProperty(Name = "subEnumValue", Reference = "entityFake/enumValue")]
    public AccessibilityLevels SubEnumValue { get; set; }

    /// <summary>
    /// Gets or sets a configuration entity resolved via an absolute or parent reference.
    /// </summary>
    [BdoProperty(Name = "configEntityFake", Reference = "^$entityFake/node1")]
    public WrapperClassFake ConfigEntityFake { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MetaWrapperFake"/> class.
    /// </summary>
    public MetaWrapperFake() : base()
    {
    }

    #endregion
}