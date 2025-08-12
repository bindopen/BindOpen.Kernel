using AutoMapper;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Data.Schema;
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

        public static MetaDataDb ToDb(
            this IBdoMetaObject poco,
            DataDbContext context)
        {
            return ToDb(poco as IBdoMetaData, context);
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

            if (typeof(ScriptwordDb).IsAssignableFrom(typeof(T))
                && dbItem is ScriptwordDb scriptDb
                && poco is IBdoScriptword script)
            {
                ScriptwordDbConverter.UpdateFromPoco(scriptDb, script, context);

                return dbItem;
            }
            else if (poco is IBdoMetaScalar scalar)
            {
                dbItem.Reference = scalar.Reference.ToDb(context);
                dbItem.Schema = scalar.Schema.ToDb(context);
                dbItem.Items = scalar.GetDataList<object>()?.Select(q => q.ToString(dbItem.ValueType)).ToList();
            }
            else if (poco is IBdoMetaNode node)
            {
                dbItem.Reference = node.Reference.ToDb(context);
                dbItem.Schema = node.Schema.ToDb(context);

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
                                dbSubItem.MetaParent = dbItem;
                                dbSubItem.MetaParentId = dbItem?.Identifier;
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
            if (poco.Schema?.DataType.ValueType == poco.DataType?.ValueType)
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

            if (dbItem is ScriptwordDb script)
            {
                return ScriptwordDbConverter.ToPoco(script);
            }
            else
            {
                switch (dbItem.Kind)
                {
                    case BdoMetaDataKind.Object:
                        BdoMetaObject obj = new()
                        {
                            Reference = dbItem?.Reference.ToPoco(),

                            DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
                            {
                                DefinitionUniqueName = dbItem.DefinitionUniqueName,
                                Identifier = dbItem.Identifier,
                                ValueType = dbItem.ValueType
                            },
                            Schema = dbItem.Schema.ToPoco()
                        };

                        obj.With(dbItem.MetaItems?.Select(q =>
                        {
                            var item = q.ToPoco();
                            if (item != null)
                            {
                                item.Parent = obj;
                            }
                            return item;
                        }).ToArray());
                        return obj;
                    case BdoMetaDataKind.Scalar:
                        BdoMetaScalar scalar = new()
                        {
                            Reference = dbItem?.Reference.ToPoco(),
                            DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
                            {
                                DefinitionUniqueName = dbItem.DefinitionUniqueName,
                                Identifier = dbItem.Identifier,
                                ValueType = dbItem.ValueType
                            },
                            Schema = dbItem.Schema.ToPoco()
                        };

                        var objects = dbItem.Items?.Select(q => q.ToObject(scalar.DataType.ValueType)).ToList();
                        scalar.WithData(objects);
                        return scalar;
                    case BdoMetaDataKind.Node:
                        BdoMetaNode node = new()
                        {
                            Reference = dbItem?.Reference.ToPoco(),
                            DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
                            {
                                DefinitionUniqueName = dbItem.DefinitionUniqueName,
                                Identifier = dbItem.Identifier,
                                ValueType = dbItem.ValueType
                            },
                            Schema = dbItem.Schema.ToPoco()
                        };

                        node.With(dbItem.MetaItems?.Select(q =>
                        {
                            var item = q.ToPoco();
                            if (item != null)
                            {
                                item.Parent = node;
                            }
                            return item;
                        }).ToArray());
                        return node;
                }
            }

            return null;
        }
    }
}
