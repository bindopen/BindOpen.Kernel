namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoSpecExtensions
    {
        public static T WithChildren<T>(this T parent, params IBdoSpec[] children)
            where T : IBdoSpec
        {
            return parent.WithChildren<T, IBdoSpec>(children);
        }

        public static T AddChildren<T>(this T parent, params IBdoSpec[] children) where T : IBdoSpec
        {
            return parent.AddChildren<T, IBdoSpec>(children);
        }

        public static IBdoSpec ToSpec(
            this IBdoMetaData meta,
            string name = null,
            bool onlyMetaAttributes = true)
            => ToSpec<BdoSpec>(meta, name, onlyMetaAttributes);

        public static T ToSpec<T>(
            this IBdoMetaData meta,
            string name = null,
            bool onlyMetaAttributes = true)
            where T : IBdoSpec, new()
        {
            T spec = default;

            if (meta != null)
            {
                var metaComposite = meta as IBdoMetaNode;
                spec = BdoData.NewSpec<T>();

                if (spec != null)
                {
                    spec.Update(meta.Spec);
                    spec.Name = name;
                    spec.Name ??= meta.Name;
                    spec.DataType = meta.DataType;

                    if (metaComposite != null)
                    {
                        foreach (var subMeta in metaComposite)
                        {
                            var subSpec = subMeta.ToSpec<T>(null, onlyMetaAttributes);
                            spec.AddChildren(subSpec);
                        }
                    }
                }
            }

            return spec;
        }
    }
}