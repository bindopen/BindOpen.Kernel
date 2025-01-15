using BindOpen.Data.Meta;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data element set.
/// </summary>
public static partial class BdoSchemaExtensions
{
    public static T WithChildren<T>(this T parent, params IBdoSchema[] children)
        where T : IBdoSchema
    {
        return parent.WithChildren<T, IBdoSchema>(children);
    }

    public static T AddChildren<T>(this T parent, params IBdoSchema[] children) where T : IBdoSchema
    {
        return parent.AddChildren<T, IBdoSchema>(children);
    }

    public static IBdoSchema ToSpec(
        this IBdoMetaData meta,
        string name = null,
        bool onlyMetaAttributes = true)
        => ToSpec<BdoSchema>(meta, name, onlyMetaAttributes);

    public static T ToSpec<T>(
        this IBdoMetaData meta,
        string name = null,
        bool onlyMetaAttributes = true)
        where T : IBdoSchema, new()
    {
        T schema = default;

        if (meta != null)
        {
            var metaComposite = meta as IBdoMetaNode;
            schema = BdoData.NewSchema<T>();

            if (schema != null)
            {
                schema.Update(meta.Schema);
                schema.Name = name;
                schema.Name ??= meta.Name;
                schema.DataType = meta.DataType;

                if (metaComposite != null)
                {
                    foreach (var subMeta in metaComposite)
                    {
                        var subSpec = subMeta.ToSpec<T>(null, onlyMetaAttributes);
                        schema.AddChildren(subSpec);
                    }
                }
            }
        }

        return schema;
    }
}