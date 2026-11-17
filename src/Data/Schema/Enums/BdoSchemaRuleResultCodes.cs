namespace BindOpen.Data.Schema;

/// <summary>
/// Provides a set of standardized result codes for schema rule evaluation.
/// These codes are used in logs and reports to identify specific validation failures.
/// </summary>
public static class BdoSchemaRuleResultCodes
{
    /// <summary>
    /// Indicates that a required metadata element is missing.
    /// </summary>
    public static readonly string ElementMissing = "Missing";

    /// <summary>
    /// Indicates that a metadata element is present but forbidden by the schema.
    /// </summary>
    public static readonly string ElementForbidden = "ElementForbidden";

    /// <summary>
    /// Indicates that an expected item within a metadata scalar or set is missing.
    /// </summary>
    public static readonly string ItemMissing = "ItemMissing";

    /// <summary>
    /// Indicates that an item within a metadata element is forbidden.
    /// </summary>
    public static readonly string ItemForbidden = "ItemForbidden";

    /// <summary>
    /// Indicates that the data format or type does not match the schema specification.
    /// </summary>
    public static readonly string InvalidData = "InvalidData";

    /// <summary>
    /// Indicates that the number of items (count) is outside the allowed range (Min/Max).
    /// </summary>
    public static readonly string BadItemNumber = "BadItemNumber";

    /// <summary>
    /// Indicates that an item's value is not among the allowed values (e.g., failed an Enum check).
    /// </summary>
    public static readonly string ItemNotAllowedValue = "ItemNotAllowedValue";
}