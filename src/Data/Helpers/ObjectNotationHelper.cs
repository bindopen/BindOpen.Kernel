using System.Data;

namespace BindOpen.Data.Helpers;

/// <summary>
/// This class provides helper methods for object notation and ADO.NET data structures.
/// </summary>
public static class ObjectNotationHelper
{
    /// <summary>
    /// Clones the specified data row into the provided data table.
    /// </summary>
    /// <param name="dataRow">The source data row to clone.</param>
    /// <param name="dataTable">The target data table that will receive the cloned row.</param>
    /// <returns>Returns the newly created DataRow if successful; otherwise, null.</returns>
    /// <remarks>
    /// The target table must have columns with names that match the source row's table columns.
    /// </remarks>
    public static DataRow CloneDataRow(DataRow dataRow, DataTable dataTable)
    {
        // Note: Fixed the bitwise OR '|' to a logical OR '||' for short-circuit evaluation
        if (dataRow == null || dataTable == null)
            return null;

        DataRow cloneDataRow = dataTable.NewRow();

        // Map data by column name to ensure compatibility between tables
        foreach (DataColumn currentDataColumn in dataRow.Table.Columns)
        {
            if (dataTable.Columns.Contains(currentDataColumn.ColumnName))
            {
                cloneDataRow[currentDataColumn.ColumnName] = dataRow[currentDataColumn.ColumnName];
            }
        }

        return cloneDataRow;
    }
}