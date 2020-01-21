using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        /// <summary>
        /// Gets the BindOpen script corresponding to the specified field.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static string GetBdoScript(DbField field)
        {
            return "$" +
                (string.IsNullOrEmpty(field.DataModule) ? "" : ("sqlDatabase('" + field.DataModule + "').")) +
                (string.IsNullOrEmpty(field.Schema) ? "" : ("sqlSchema('" + field.Schema + "').")) +
                (string.IsNullOrEmpty(field.DataTable) ? "" : ("sqlTable('" + field.DataTable + "').")) +
                (string.IsNullOrEmpty(field.Name) ? "" : ("sqlField('" + field.Name + "')"));
        }

        /// <summary>
        /// Gets the BindOpen script corresponding to the specified table.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static string GetBdoScript(DbTable table)
        {
            return "$" +
                (string.IsNullOrEmpty(table.DataModule) ? "" : ("sqlDatabase('" + table.DataModule + "').")) +
                (string.IsNullOrEmpty(table.Schema) ? "" : ("sqlSchema('" + table.Schema + "').")) +
                (string.IsNullOrEmpty(table.Name) ? "" : ("sqlTable('" + table.Name + "')."));
        }
    }
}