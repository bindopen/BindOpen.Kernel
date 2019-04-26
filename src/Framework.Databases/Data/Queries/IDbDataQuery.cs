using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IDbDataQuery: IDataItem
    {
        string DataModule { get; set; }
        string DataTable { get; set; }
        string DataTableAlias { get; set; }
        List<DbField> Fields { get; set; }
        bool IsTrackingEnabled { get; set; }
        DbDataQueryKind Kind { get; set; }
        string Name { get; set; }
        string Schema { get; set; }

        DbField GetDataFieldWithName(string name);
        DbField GetFieldWithBoundFieldName(string boundFieldName);
        List<DbField> GetForeignKeyDataFields();
        List<DbField> GetKeyDataFields();
        List<DbField> GetPrimaryKeyDataFields();
    }
}