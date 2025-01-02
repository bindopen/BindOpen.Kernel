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
        public static MetaDataDb ToDb(this IBdoMetaData poco)
        {
            if (poco == null) return null;

            if (poco is IBdoScriptword script)
            {
                var dbItem = ScriptwordDbConverter.ToDb(script);
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

                dbItem.UpdateFromPoco(poco);
                return dbItem;
            }
        }

        public static MetaDataDb UpdateFromPoco(
            this MetaDataDb dbItem,
            IBdoMetaData poco)
        {
            if (poco == null) return null;

            MapperConfiguration config;
            Mapper mapper;

            if (dbItem is ScriptwordDb scriptDb && poco is IBdoScriptword script)
            {
                ScriptwordDbConverter.UpdateFromPoco(scriptDb, script);
            }
            else if (poco is IBdoMetaObject obj)
            {
                config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoMetaObject, MetaDataDb>()
                        .ForMember(q => q.ClassReference, opt => opt.Ignore())
                        .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDb()))
                        //.ForMember(q => q.Item, opt => opt.Ignore())
                        .ForMember(q => q.MetaItems, opt => opt.Ignore())
                        .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDb()))
                );

                mapper = new Mapper(config);
                mapper.Map(obj, dbItem);

                dbItem.ClassReference = obj.DataType.IsSpecified() ? obj?.DataType.ToDb() : null;
                dbItem.DefinitionUniqueName = obj?.DataType?.DefinitionUniqueName;

                dbItem.MetaItems = obj.Items?.Select(q => q.ToDb()).ToList();
                dbItem.ValueType = obj?.DataType.ValueType ?? DataValueTypes.Any;
                if (obj.Spec?.DataType.ValueType == obj.DataType?.ValueType
                    || obj.DataType.ValueType == DataValueTypes.Object)
                {
                    dbItem.ValueType = DataValueTypes.Any;
                }
                if (dbItem.Spec?.ValueType == DataValueTypes.Object)
                {
                    dbItem.Spec.ValueType = DataValueTypes.Any;
                }
            }
            else if (poco is IBdoMetaScalar scalar)
            {
                config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoMetaScalar, MetaDataDb>()
                        .ForMember(q => q.ClassReference, opt => opt.Ignore())
                        .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDb()))
                        .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDb()))
                );

                mapper = new Mapper(config);
                mapper.Map(scalar, dbItem);

                dbItem.ClassReference = scalar.DataType.IsSpecified() ? scalar?.DataType.ToDb() : null;
                dbItem.DefinitionUniqueName = scalar?.DataType?.DefinitionUniqueName;

                dbItem.Items = scalar.GetDataList<object>()?.Select(q => q.ToString(dbItem.ValueType)).ToList();

                dbItem.ValueType = scalar?.DataType.ValueType ?? DataValueTypes.Any;
                if (scalar.Spec?.DataType.ValueType == scalar.DataType?.ValueType)
                {
                    dbItem.ValueType = DataValueTypes.Any;
                }
            }
            else if (poco is IBdoMetaNode set)
            {
                config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoMetaNode, MetaDataDb>()
                        .ForMember(q => q.ClassReference, opt => opt.Ignore())
                        .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDb()))
                        .ForMember(q => q.MetaItems, opt => opt.Ignore())
                        .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDb()))
                );

                mapper = new Mapper(config);
                mapper.Map(set, dbItem);

                dbItem.ClassReference = set.DataType.IsSpecified() ? set?.DataType.ToDb() : null;
                dbItem.DefinitionUniqueName = set?.DataType?.DefinitionUniqueName;

                dbItem.MetaItems = set.Items?.Select(q => q.ToDb()).ToList();
                dbItem.ValueType = set?.DataType.ValueType ?? DataValueTypes.Any;
                if (set.Spec?.DataType.ValueType == set.DataType?.ValueType)
                {
                    dbItem.ValueType = DataValueTypes.Any;
                }
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
