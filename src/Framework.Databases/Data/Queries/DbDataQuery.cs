using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a database data query.
    /// </summary>
    public abstract class DbDataQuery : DataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<DbField> _Fields = new List<DbField>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates whether the tracking is enabled for this instance.
        /// </summary>
        public Boolean IsTrackingEnabled { get; set; } = false;

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
        public DbDataQueryKind Kind { get; set; } = DbDataQueryKind.Select;

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<DbField> Fields
        {
            get { return this._Fields; }
            set { this._Fields = new List<DbField>(value); }
        }

        ///// <summary>
        ///// Exeuction condition of this instance.
        ///// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public DataExpression ExecutionCondition
        //{
        //    get { return this._ExecutionCondition; }
        //    set { this._ExecutionCondition = value; }
        //}

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbDataQuery class.
        /// </summary>
        public DbDataQuery()
        {
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
        public DbField GetFieldWithBoundFieldName(String boundFieldName)
        {
            if ((boundFieldName!=null)&(this._Fields!=null))
                foreach (DbField rField in this._Fields)
                    if (rField.GetName().Equals(boundFieldName, StringComparison.OrdinalIgnoreCase))
                        return rField;
            return null;
        }

        /// <summary>
        /// Gets the data field with the specified data field name.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <returns>The data field with the specified data field name.</returns>
        public DbField GetDataFieldWithName(String name)
        {
            foreach (DbField rField in this._Fields)
            {
                if (rField.Alias.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return rField;
                if (rField.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return rField;
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
            foreach (DbField rField in this._Fields)
            {
                if ((rField.IsKey == true)|(rField.IsForeignKey == true))
                    fields.Add(rField);
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
            foreach (DbField rField in this._Fields)
            {
                if (rField.IsKey == true)
                    fields.Add(rField);
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
            foreach (DbField rField in this._Fields)
            {
                if (rField.IsForeignKey == true)
                    fields.Add(rField);
            }
            return fields;
        }

        #endregion
    }
}