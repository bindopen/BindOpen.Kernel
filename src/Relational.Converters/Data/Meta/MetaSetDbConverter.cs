using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Db converter of meta sets.
    /// </summary>
    public static class MetaSetDbConverter
    {
        /// <summary>
        /// Converts an expression poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaSetDb ToDb(
            this IBdoMetaSet poco,
            DataDbContext context)
        {
            if (poco == null) return null;

            MetaSetDb dbItem = new();
            dbItem.UpdateFromPoco(poco, context);

            return dbItem;
        }

        public static T UpdateFromPoco<T>(
            this T dbItem,
            IBdoMetaSet poco,
            DataDbContext context)
            where T : MetaSetDb
        {
            if (dbItem == null) return null;

            if (poco == null) return dbItem;

            poco.Identifier ??= StringHelper.NewGuid();
            poco.UpdateTrees();

            dbItem.Identifier = dbItem.Identifier;
            dbItem.Name = dbItem.Name;

            if (context != null)
            {
                dbItem.Items ??= [];
                dbItem.Items.RemoveAll(q => poco.Items?.Any(p => p?.Identifier == q?.Identifier) != true);

                if (poco?.Items?.Count > 0)
                {
                    foreach (var subItem in poco.Items)
                    {
                        var dbSubItem = context.Upsert(subItem);

                        if (dbItem.Items.Any(p => p?.Identifier == dbSubItem?.Identifier) != true)
                        {
                            dbItem.Items.Add(dbSubItem);
                        }
                    }
                }
            }

            return dbItem;
        }

        /// <summary>
        /// Converts a meta set DTO to a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoMetaSet ToPoco(
            this MetaSetDb dbItem)
        {
            if (dbItem == null) return null;

            BdoMetaSet poco = [];
            poco.Identifier = dbItem.Identifier;
            poco.Name = dbItem.Name;
            poco.With(dbItem.Items?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
