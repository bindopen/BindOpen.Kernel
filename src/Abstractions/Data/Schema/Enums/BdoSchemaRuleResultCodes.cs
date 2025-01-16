namespace BindOpen.Data.Schema;

/// <summary>
/// This enumeration represents the possible schema rule result codes.
/// </summary>
public static class BdoSchemaRuleResultCodes
{
    public static readonly string ElementMissing = "Missing";

    public static readonly string ElementForbidden = "ElementForbidden";

    public static readonly string ItemMissing = "ItemMissing";

    public static readonly string ItemForbidden = "ItemForbidden";

    public static readonly string InvalidData = "InvalidData";

    public static readonly string BadItemNumber = "BadItemNumber";

    public static readonly string ItemNotAllowedValue = "ItemNotAllowedValue";
}
