using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Expression
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    [Serializable()]
    [XmlType("DataExpression", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dataExpression", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
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
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [XmlElement("kind")]
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
    }
}