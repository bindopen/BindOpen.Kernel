using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class represents a Db converter of script words.
/// </summary>
public static class ConnectorDbConverter
{
    /// <summary>
    /// Converts a script word poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ConnectorDb ToDb(
        this IBdoConnector poco,
        DataDbContext context,
        bool root = true)
    {
        if (poco == null) return null;

        ConnectorDb dbItem = new();

        if (root) poco = poco?.Root() as IBdoConnector;

        dbItem.UpdateFromPoco(poco, context);

        return dbItem;
    }

    public static ConnectorDb UpdateFromPoco(
        this ConnectorDb dbItem,
        IBdoConnector poco,
        DataDbContext context)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        MetaDataDbConverter.UpdateFromPoco<MetaDataDb>(dbItem, poco, context);
        dbItem.ValueType = DataValueTypes.Any;
        dbItem.TokenKind = poco.TokenKind;
        dbItem.Text = poco.Text;

        if (context == null)
        {
            dbItem.Child = poco.Child.ToDb(context, false);
        }
        else
        {
            dbItem.Child = context.Upsert(poco.Child);
        }

        if (dbItem.Child != null)
        {
            dbItem.Child.Parent = dbItem;
            dbItem.Child.ParentId = dbItem.Identifier;
        }

        return dbItem;
    }

    /// <summary>
    /// Converts a script word DTO into a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoConnector ToPoco(
        this ConnectorDb dbItem)
    {
        if (dbItem == null) return null;

        BdoConnector poco = new()
        {
            DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
            {
                DefinitionUniqueName = dbItem.DefinitionUniqueName,
                Identifier = dbItem.Identifier,
                ValueType = DataValueTypes.Connector
            },
            Schema = dbItem.Schema.ToPoco(),
            ExpressionKind = BdoExpressionKind.Word
        };

        poco.WithChild(dbItem.Child.ToPoco());
        poco.With(dbItem.MetaItems?.Select(
            q =>
            {
                var item = q.ToPoco();
                if (item != null)
                {
                    item.Parent = poco;
                }
                return item;
            }).ToArray());

        return poco;
    }
}
