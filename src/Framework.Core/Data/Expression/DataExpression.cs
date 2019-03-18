using BindOpen.Framework.Core.Data.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Expression
{

    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    [Serializable()]
    [XmlType("DataExpression", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dataExpression", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataExpression : DataItem
    {
   
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private String _Text = null;
        private DataExpressionKind _Kind = DataExpressionKind.Auto;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The text of this instance.
        /// </summary>
        [XmlElement("value")]
        public virtual String Text
        {
            get { return this._Text; }
            set { this._Text = value; }
        }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [XmlElement("kind")]
        public DataExpressionKind Kind
        {
            get { return this._Kind; }
            set { this._Kind = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataExpression class.
        /// </summary>
        public DataExpression()
        {
        }

        /// <summary>
        /// Instantiates a new instance of DataExpression class.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <param name="kind">The data expresion kind to consider.</param>
        public DataExpression(String text=null, DataExpressionKind kind = DataExpressionKind.Script)
        {
            this._Text = text;
            this._Kind = kind;
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Creates a new script expression.
        /// </summary>
        /// <param name="text">The script text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateScript(String text)
        {
            return new DataExpression(text, DataExpressionKind.Script);
        }

        /// <summary>
        /// Creates a new literal expression.
        /// </summary>
        /// <param name="text">The literal text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateLiteral(String text)
        {
            return new DataExpression(text, DataExpressionKind.Literal);
        }

        #endregion

    }
}