using AutoMapper;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Db converter of meta data items.
    /// </summary>
    public static class MetaDataDbConverter
    {
        /// <summary>
        /// Converts a metadata poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaDataDb ToDb(
            this IBdoMetaData poco,
            DataDbContext context)
        {
            if (poco == null) return null;

            if (poco is IBdoScriptword script)
            {
                var dbItem = ScriptwordDbConverter.ToDb(script, context);
                return dbItem;
            }
            else
            {
                var dbItem = new MetaDataDb();

                if (poco is IBdoMetaObject)
                {
                    dbItem.Kind = BdoMetaDataKind.Object;
                }
                else if (poco is IBdoMetaScalar)
                {
                    dbItem.Kind = BdoMetaDataKind.Scalar;
                }
                else if (poco is IBdoMetaNode)
                {
                    dbItem.Kind = BdoMetaDataKind.Node;
                }

                dbItem.UpdateFromPoco(poco, context);
                return dbItem;
            }
        }

        public static T UpdateFromPoco<T>(
            this T dbItem,
            IBdoMetaData poco,
            DataDbContext context)
            where T : MetaDataDb
        {
            if (poco == null) return null;

            poco.Identifier ??= StringHelper.NewGuid();

            MapperConfiguration config;
            Mapper mapper;

            if (typeof(T).IsAssignableFrom(typeof(ScriptwordDb))
                && dbItem is ScriptwordDb scriptDb
                && poco is IBdoScriptword script)
            {
                ScriptwordDbConverter.UpdateFromPoco(scriptDb, script, context);

                return dbItem;
            }
            else if (poco is IBdoMetaScalar scalar)
            {
                config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoMetaScalar, T>()
                        .ForMember(q => q.ClassReference, opt => opt.Ignore())
                        .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDb(context)))
                        .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDb(context)))
                );

                mapper = new Mapper(config);
                mapper.Map(scalar, dbItem);

                dbItem.Items = scalar.GetDataList<object>()?.Select(q => q.ToString(dbItem.ValueType)).ToList();
            }
            else if (poco is IBdoMetaNode node)
            {
                config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoMetaNode, T>()
                        .ForMember(q => q.ClassReference, opt => opt.Ignore())
                        .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDb(context)))
                        .ForMember(q => q.MetaItems, opt => opt.Ignore())
                        .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDb(context)))
                );

                mapper = new Mapper(config);
                mapper.Map(node, dbItem);

                if (context != null)
                {
                    dbItem.MetaItems ??= [];
                    dbItem.MetaItems.RemoveAll(q => node.Items?.Any(p => p?.Identifier == q?.Identifier) != true);

                    if (node?.Items?.Count > 0)
                    {
                        foreach (var subItem in node.Items)
                        {
                            var dbSubItem = context.Upsert(subItem);

                            if (dbItem.MetaItems.Any(p => p?.Identifier == dbSubItem?.Identifier) != true)
                            {
                                dbItem.MetaItems.Add(dbSubItem);
                            }
                        }
                    }
                }
            }

            if (poco?.DataType != null)
            {
                poco.DataType.Identifier ??= poco.Identifier;
            }

            dbItem.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;
            dbItem.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;
            if (poco.Spec?.DataType.ValueType == poco.DataType?.ValueType)
            {
                dbItem.ValueType = DataValueTypes.Any;
            }

            if (context == null)
            {
                dbItem.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDb() : null;
            }
            else
            {
                dbItem.ClassReference = context.Upsert(poco.DataType);
            }

            return dbItem;
        }

        /// <summary>
        /// Converts a meta data DTO to a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoMetaData ToPoco(
            this MetaDataDb dbItem)
        {
            if (dbItem == null) return null;

            MapperConfiguration config;
            Mapper mapper;

            if (dbItem is ScriptwordDb script)
            {
                return ScriptwordDbConverter.ToPoco(script);
            }
            else
            {
                switch (dbItem.Kind)
                {
                    case BdoMetaDataKind.Object:
                        config = new MapperConfiguration(
                            cfg => cfg.CreateMap<MetaDataDb, BdoMetaObject>()
                                .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                                .ForMember(q => q.DataType, opt => opt.Ignore())
                                .ForMember(q => q.Items, opt => opt.Ignore())
                                .ForMember(q => q.Parent, opt => opt.Ignore())
                                .ForMember(q => q.Spec, opt => opt.Ignore())
                            );

                        mapper = new Mapper(config);
                        var obj = mapper.Map<BdoMetaObject>(dbItem);

                        obj.DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
                        {
                            DefinitionUniqueName = dbItem.DefinitionUniqueName,
                            Identifier = dbItem.Identifier,
                            ValueType = dbItem.ValueType
                        };
                        obj.Spec = dbItem.Spec.ToPoco();

                        obj.With(dbItem.MetaItems?.Select(q => q.ToPoco()).ToArray());
                        return obj;
                    case BdoMetaDataKind.Scalar:
                        config = new MapperConfiguration(
                        cfg => cfg.CreateMap<MetaDataDb, BdoMetaScalar>()
                            .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                            .ForMember(q => q.DataType, opt => opt.Ignore())
                            .ForMember(q => q.Parent, opt => opt.Ignore())
                            .ForMember(q => q.Spec, opt => opt.Ignore())
                        );

                        mapper = new Mapper(config);
                        var scalar = mapper.Map<BdoMetaScalar>(dbItem);

                        scalar.DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
                        {
                            DefinitionUniqueName = dbItem.DefinitionUniqueName,
                            Identifier = dbItem.Identifier,
                            ValueType = dbItem.ValueType
                        };
                        scalar.Spec = dbItem.Spec.ToPoco();

                        var objects = dbItem.Items?.Select(q => q.ToObject(scalar.DataType.ValueType)).ToList();
                        scalar.WithData(objects);
                        return scalar;
                    case BdoMetaDataKind.Node:

                        config = new MapperConfiguration(
                        cfg => cfg.CreateMap<MetaDataDb, BdoMetaNode>()
                            .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                            .ForMember(q => q.DataType, opt => opt.Ignore())
                            .ForMember(q => q.Items, opt => opt.Ignore())
                            .ForMember(q => q.Parent, opt => opt.Ignore())
                            .ForMember(q => q.Spec, opt => opt.Ignore())
                        );

                        mapper = new Mapper(config);
                        var node = mapper.Map<BdoMetaNode>(dbItem);

                        node.DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
                        {
                            DefinitionUniqueName = dbItem.DefinitionUniqueName,
                            Identifier = dbItem.Identifier,
                            ValueType = dbItem.ValueType
                        };
                        node.Spec = dbItem.Spec.ToPoco();

                        node.With(dbItem.MetaItems?.Select(q => q.ToPoco()).ToArray());
                        return node;
                }
            }

            return null;
        }
    }
}
