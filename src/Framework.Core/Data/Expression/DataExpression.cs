﻿using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;

namespace BindOpen.Framework.Core.Data.Expression
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    [Serializable()]
    [XmlType("DataExpression", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataExpression", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
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
    }
}