using BindOpen.Scoping.Script;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an expression database entity.
    /// </summary>
    [XmlType("Expression", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "expression", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ExpressionDb : IBdoDb
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [Key]
        public string Identifier { get; set; }

        /// <summary>
        /// The text of this instance.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public BdoExpressionKind ExpressionKind { get; set; } = BdoExpressionKind.Auto;

        // Reference

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        public ReferenceDb Reference { get; set; }

        // Word

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public ScriptwordDb Scriptword { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of ExpressionDb class.
        /// </summary>
        public ExpressionDb()
        {
        }

        #endregion
    }
}