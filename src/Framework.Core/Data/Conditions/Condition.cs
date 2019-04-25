﻿using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Conditions;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Conditions
{
    /// <summary>
    /// This class represents a condition.
    /// </summary>
    [Serializable()]
    [XmlType("Condition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "condition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(AdvancedCondition))]
    [XmlInclude(typeof(BasicCondition))]
    [XmlInclude(typeof(ScriptCondition))]
    public abstract class Condition : DataItem, ICondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value that expresses that the condition is satisfied.
        /// </summary>
        [XmlElement("trueValue")]
        public bool TrueValue { get; set; } = true;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessCondition class.
        /// </summary>
        protected Condition() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BusinessCondition class.
        /// </summary>
        /// <param name="trueValue">The true value to consider.</param>
        protected Condition(bool trueValue) : base()
        {
            this.TrueValue= trueValue;
        }

        #endregion
    }
}