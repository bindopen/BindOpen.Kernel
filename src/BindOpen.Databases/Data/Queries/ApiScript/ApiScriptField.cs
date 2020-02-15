using BindOpen.Data.Items;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a Api script expression.
    /// </summary>
    public class ApiScriptField : DataItem
    {
        /// <summary>
        /// The field alias of this instance.
        /// </summary>
        public string FieldAlias
        {
            get;
            set;
        } = null;

        /// <summary>
        /// The field of this instance.
        /// </summary>
        public DbField Field
        {
            get;
            set;
        } = null;

        /// <summary>
        /// Creates a new instance of the ApiScriptExpression class.
        /// </summary>
        public ApiScriptField()
        {
        }

        /// <summary>
        /// Creates a new instance of the ApiScriptExpression class.
        /// </summary>
        /// <param name="fieldAlias">The field alias to consider.</param>
        /// <param name="field">The field to consider.</param>
        public ApiScriptField(
            string fieldAlias,
            DbField field)
        {
            this.FieldAlias = fieldAlias;
            this.Field = field;
        }
    }
}
