using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a catalog el that is an el whose els are entities.
/// </summary>
public partial class BdoSchemaSet : TBdoSet<IBdoSchema>, IBdoSchemaSet
{
    // ------------------------------------------
    // CONVERTERS
    // ------------------------------------------

    #region Converters

    /// <summary>
    /// Converts from data element array.
    /// </summary>
    /// <param key="elems">The elems to consider.</param>
    public static explicit operator BdoSchemaSet(IBdoSchema[] elems)
    {
        return BdoData.NewSchemaSet(elems);
    }

    /// <summary>
    /// Converts from data element array.
    /// </summary>
    /// <param key="elems">The elems to consider.</param>
    public static explicit operator IBdoSchema[](BdoSchemaSet metaSet)
    {
        return metaSet?.ToArray();
    }

    #endregion

    // --------------------------------------------------
    // CONSTRUCTORS
    // --------------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the BdoSchemaSet class.
    /// </summary>
    public BdoSchemaSet() : base()
    {
    }

    #endregion

    // ------------------------------------------
    // INamed Implementation
    // ------------------------------------------

    #region INamed

    /// <summary>
    /// 
    /// </summary>
    [BdoProperty("name")]
    public string Name { get; set; }

    #endregion

    // --------------------------------------------------
    // IBdoSchemaSet Implementation
    // --------------------------------------------------

    #region IBdoSchemaSet

    /// <summary>
    /// Returns a text node representing this instance.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Empty;
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
    public override object Clone()
    {
        var el = base.Clone().As<BdoSchemaSet>();

        return el;
    }

    #endregion
}