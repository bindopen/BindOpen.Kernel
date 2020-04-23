using System.Data;

namespace BindOpen.Data.Helpers.Serialization
{

    /// <summary>
    /// This class represents a helper for object notation.
    /// </summary>
    public static class ObjectNotationHelper
    {

        /// <summary>
        /// Clones the specified data row into the specified data table.
        /// </summary>
        /// <param name="dataRow">The data row to clone.</param>
        /// <param name="dataTable">The data table that receives the cloned datatable.</param>
        /// <returns>The cloned data row.</returns>
        public static DataRow CloneDataRow(DataRow dataRow, DataTable dataTable)
        {
            if ((dataRow==null)|(dataTable==null))
                return null;

            DataRow cloneDataRow=dataTable.NewRow();
            foreach (DataColumn currentDataColumn in dataRow.Table.Columns)
                cloneDataRow[currentDataColumn.ColumnName] = dataRow[currentDataColumn.ColumnName];
            return cloneDataRow;
        }

    }
}
