using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Databases.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a database data query.
    /// </summary>
    public abstract class DbQuery : DataItem, IDbQuery
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<DbField> _fields = new List<DbField>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates whether the tracking is enabled for this instance.
        /// </summary>
        public bool IsTrackingEnabled { get; set; } = false;

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public string Name { get; set; } = "dataquery_" + DateTime.Now.ToString(StringHelper.__DateFormat);

        /// <summary>
        /// Name of the data module of this instance.
        /// </summary>
        public string DataModule { get; set; }

        /// <summary>
        /// Name of the data table of this instance.
        /// </summary>
        public string DataTable { get; set; }

        /// <summary>
        /// Name of the data table alias of this instance.
        /// </summary>
        public string DataTableAlias { get; set; }

        /// <summary>
        /// Schema of this instance.
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public DbQueryKind Kind { get; set; } = DbQueryKind.Select;

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<DbField> Fields
        {
            get { return this._fields; }
            set { this._fields = new List<DbField>(value); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQuery class.
        /// </summary>
        protected DbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbQuery class.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        protected DbQuery(string name)
        {
            Name = name;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the data field with the specified bound data field name.
        /// </summary>
        /// <param name="boundFieldName">Name of the bound data field.</param>
        /// <returns>The data field with the specified bound data field name.</returns>
        public DbField GetFieldWithBoundFieldName(string boundFieldName)
        {
            if ((boundFieldName != null) && (this._fields != null))
            {
                foreach (DbField field in this._fields)
                {
                    if (field.GetName().Equals(boundFieldName, StringComparison.OrdinalIgnoreCase))
                        return field;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the data field with the specified data field name.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <returns>The data field with the specified data field name.</returns>
        public DbField GetDataFieldWithName(string name)
        {
            foreach (DbField field in this._fields)
            {
                if (field.Alias.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return field;
                if (field.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return field;
            }
            return null;
        }

        /// <summary>
        /// Gets the key data fields of this instance.
        /// </summary>
        /// <returns>The key data fields of this instance.</returns>
        public List<DbField> GetKeyDataFields()
        {
            List<DbField> fields = new List<DbField>();
            foreach (DbField field in this._fields)
            {
                if ((field.IsKey) || (field.IsForeignKey))
                    fields.Add(field);
            }
            return fields;
        }

        /// <summary>
        /// Gets the primary data fields of this instance.
        /// </summary>
        /// <returns>The primary data fields of this instance.</returns>
        public List<DbField> GetPrimaryKeyDataFields()
        {
            List<DbField> fields = new List<DbField>();
            foreach (DbField field in this._fields)
            {
                if (field.IsKey)
                    fields.Add(field);
            }
            return fields;
        }

        /// <summary>
        /// Gets the foreign data fields of this instance.
        /// </summary>
        /// <returns>The foreign data fields of this instance.</returns>
        public List<DbField> GetForeignKeyDataFields()
        {
            List<DbField> fields = new List<DbField>();
            foreach (DbField field in this._fields)
            {
                if (field.IsForeignKey)
                    fields.Add(field);
            }
            return fields;
        }

        #endregion
    }
}