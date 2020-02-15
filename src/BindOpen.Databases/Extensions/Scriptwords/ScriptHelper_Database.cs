using System;
using System.Collections.Generic;

namespace BindOpen.Extensions.Scriptwords
{
    /// <summary>
    /// This class represents a script helper concerning database.
    /// </summary>
    public static class ScriptHelper_Database
    {
        /// <summary>
        /// Gets the BdO filter string that filters number values.
        /// </summary>
        /// <param name="sqlDbFieldString">The Sql script representing the data field to consider.</param>
        /// <param name="values">Values used to filter.</param>
        /// <returns>The string that allows to filter users and workgroup users.</returns>
        private static String GetSqlNumberFilterString(
            String sqlDbFieldString,
            List<string> values)
        {

            // we build the user filter condition string
            String userFilterString = "";
            foreach (String stringValue in values)
                if (stringValue == "*")
                    return "$SqlEq(1,1)";
                else if (stringValue.Contains("-"))
                {
                    int index = stringValue.IndexOf("-");
                    long aMinValue = -1;
                    long aMaxValue = -1;
                    String aMinStringValue = stringValue.Substring(0, index);
                    String aMaxStringValue = stringValue.Substring(index + 1);
                    if ((long.TryParse(aMinStringValue, out aMinValue)) &
                        (long.TryParse(aMaxStringValue, out aMaxValue)))
                    {
                        if (userFilterString != "")
                            userFilterString += ",";
                        userFilterString += "$SqlAnd(";
                        userFilterString += "  $SqlGreater(" +
                            sqlDbFieldString + "," +
                            aMinStringValue + "),";
                        userFilterString += "  $SqlLess(" + sqlDbFieldString + "," + aMaxStringValue + ")";
                        userFilterString += ")";
                    }
                }
                else
                {
                    long value = -1;
                    if (long.TryParse(stringValue, out value))
                    {
                        if (userFilterString != "")
                            userFilterString += ",";
                        userFilterString += "$SqlEq(" +
                            sqlDbFieldString + "," +
                            stringValue + ")";
                    }
                }

            return userFilterString;
        }

        /// <summary>
        /// Gets the BdO filter string that AND filters number values.
        /// </summary>
        /// <param name="sqlDbFieldString">The Sql script representing the data field to consider.</param>
        /// <param name="values">Values used to filter.</param>
        /// <returns>The string that allows to filter users and workgroup users.</returns>
        public static string GetSqlNumberAndFilterString(
            String sqlDbFieldString,
            List<string> values)
        {
            return "$SqlAnd(" + ScriptHelper_Database.GetSqlNumberFilterString(sqlDbFieldString, values) + ")";
        }

        /// <summary>
        /// Gets the BdO filter string that AND filters number values.
        /// </summary>
        /// <param name="sqlDbFieldString">The Sql script representing the data field to consider.</param>
        /// <param name="values">Values used to filter.</param>
        /// <returns>The string that allows to filter users and workgroup users.</returns>
        public static string GetSqlNumberOrFilterString(
            String sqlDbFieldString,
            List<string> values)
        {
            return "$SqlOr(" + ScriptHelper_Database.GetSqlNumberFilterString(sqlDbFieldString, values) + ")";
        }

        /// <summary>
        /// Gets the BdO filter string that filters text values.
        /// </summary>
        /// <param name="sqlDbFieldString">The Sql script representing the data field to consider.</param>
        /// <param name="values">Values used to filter.</param>
        /// <returns>The string that allows to filter users and workgroup users.</returns>
        private static String GetSqlTextFilterString(
            String sqlDbFieldString,
            List<string> values)
        {

            // we build the user filter condition string
            String userFilterString = "";
            foreach (String stringValue in values)
                if (stringValue == "*")
                    return "$SqlEq(1,1)";
                else if (stringValue.Contains("-"))
                {
                    int index = stringValue.IndexOf("-");
                    String aMinStringValue = stringValue.Substring(0, index);
                    String aMaxStringValue = stringValue.Substring(index + 1);

                    if (userFilterString != "")
                        userFilterString += ",";
                    userFilterString += "$SqlAnd(";
                    userFilterString += "  $SqlGreater(" +
                        sqlDbFieldString + "," +
                        "$SqlText(\"" + aMinStringValue + "\") ),";
                    userFilterString += "  $SqlLess(" +
                        sqlDbFieldString + "," +
                        "$SqlText(\"" + aMaxStringValue + "\") )";
                    userFilterString += ")";

                }
                else
                {
                    if (userFilterString != "")
                        userFilterString += ",";
                    userFilterString += "$SqlEq(" +
                        sqlDbFieldString + "," +
                        "$SqlText(\"" + stringValue + "\")" +
                        ")";
                }

            return userFilterString;
        }

        /// <summary>
        /// Gets the BdO filter string that AND filters text values.
        /// </summary>
        /// <param name="sqlDbFieldString">The Sql script representing the data field to consider.</param>
        /// <param name="values">Values used to filter.</param>
        /// <returns>The string that allows to filter users and workgroup users.</returns>
        public static string GetSqlTextAndFilterString(
            String sqlDbFieldString,
            List<string> values)
        {
            return "$SqlAnd(" + ScriptHelper_Database.GetSqlTextFilterString(sqlDbFieldString, values) + ")";
        }

        /// <summary>
        /// Gets the BdO filter string that AND filters text values.
        /// </summary>
        /// <param name="sqlDbFieldString">The Sql script representing the data field to consider.</param>
        /// <param name="values">Values used to filter.</param>
        /// <returns>The string that allows to filter users and workgroup users.</returns>
        public static string GetSqlTextOrFilterString(
            String sqlDbFieldString,
            List<string> values)
        {
            return "$SqlOr(" + ScriptHelper_Database.GetSqlTextFilterString(sqlDbFieldString, values) + ")";
        }
    }
}