using BindOpen.Framework.Data.Helpers.Strings;
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
            string st = "";

            if (field != null)
            {
                st = GetBdoScript(DbFluent.Table(field.DataTable, field.Schema, field.DataModule).WithAlias(field.DataTableAlias))
                    .ConcatenateIfFirstNotEmpty(".");

                if (!string.IsNullOrEmpty(field.Alias))
                {
                    st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlField('" + field.Alias + "')";
                }
                else if (!string.IsNullOrEmpty(field.Name))
                {
                    st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlField('" + field.Name + "')";
                }
            }

            return st;
        }

        /// <summary>
        /// Gets the BindOpen script corresponding to the specified table.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static string GetBdoScript(DbTable table)
        {
            string st = "";

            if (table != null)
            {
                st.ConcatenateIf(!string.IsNullOrEmpty(table.DataModule), st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlDatabase('" + table.DataModule + "').");

                st.ConcatenateIf(!string.IsNullOrEmpty(table.Schema), st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlSchema('" + table.Schema + "').");

                if (!string.IsNullOrEmpty(table.Alias))
                {
                    st += st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlTable('" + table.Alias + "')";
                }
                else if (!string.IsNullOrEmpty(table.Name))
                {
                    st += st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlTable('" + table.Name + "')";
                }
            }

            return st;
        }
    }
}