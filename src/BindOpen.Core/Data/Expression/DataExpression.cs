using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System.Xml.Serialization;

namespace BindOpen.Data.Expression
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    [XmlType("DataExpression", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot(ElementName = "dataExpression", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
    public class DataExpression : DataItem, IDataExpression
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [XmlElement("value")]
        [DetailPropertyAttribute]
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [XmlElement("kind")]
        [DetailPropertyAttribute]
        public DataExpressionKind Kind { get; set; } = DataExpressionKind.Auto;

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
            return this;
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
            if (expression != null)
            {
                return expression.Text;
            }

            return "";
        }

        #endregion
    }
}