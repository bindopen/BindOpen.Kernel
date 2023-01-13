using BindOpen.Extensions.Scripting;
using BindOpen.MetaData.Elements;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public class BdoExpression : BdoItem, IBdoExpression
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [BdoMeta]
        [XmlElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [BdoMeta]
        [XmlElement("kind")]
        public BdoExpressionKind Kind { get; set; } = BdoExpressionKind.Auto;

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [BdoMeta]
        [XmlElement("word")]
        public IBdoScriptword Word { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataExpression class.
        /// </summary>
        public BdoExpression()
        {
        }

        #endregion


        // -----------------------------------------------
        // Converters
        // -----------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        public static explicit operator string(BdoExpression exp)
        {
            return exp?.ToString();
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        public IBdoExpression AsScript()
        {
            Kind = BdoExpressionKind.Script;
            return this;
        }

        public IBdoExpression AsLiteral()
        {
            Kind = BdoExpressionKind.Literal;
            return this;
        }

        public IBdoExpression AsAuto()
        {
            Kind = BdoExpressionKind.Auto;
            return this;
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
            return Kind switch
            {
                BdoExpressionKind.Auto or BdoExpressionKind.Literal or BdoExpressionKind.Script => Text,
                BdoExpressionKind.Word => Word.ToString(),
                _ => null,
            };
        }

        #endregion
    }
}