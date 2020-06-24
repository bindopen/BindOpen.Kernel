using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System.Xml.Serialization;

namespace BindOpen.Data.Expression
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    [XmlType("DataExpression", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataExpression", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DataExpression : DataItem, IDataExpression
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [DetailPropertyAttribute]
        [XmlElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [DetailPropertyAttribute]
        [XmlElement("kind")]
        public DataExpressionKind Kind { get; set; } = DataExpressionKind.Auto;

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [DetailPropertyAttribute]
        [XmlElement("word")]
        public BdoScriptword Word { get; set; }

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

        #endregion

        // ------------------------------------------
        // ACCCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (Kind)
            {
                case DataExpressionKind.Auto:
                case DataExpressionKind.Literal:
                case DataExpressionKind.Script:
                    return Text;
                case DataExpressionKind.Word:
                    return Word.ToString();
            }

            return null;
        }

        #endregion

        // ------------------------------------------
        // OPERATORS
        // ------------------------------------------

        #region Operators

        /// <summary>
        /// Returns the data expression string corresponding to this instance.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        public static implicit operator string(DataExpression expression)
        {
            return expression?.ToString();
        }

        #endregion
    }
}