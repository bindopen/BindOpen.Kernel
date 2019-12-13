using System;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.System.Diagnostics.Events
{
    /// <summary>
    /// This class represents a conditional event.
    /// </summary>
    [XmlType("ConditionalEvent", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "event", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoConditionalEvent : BdoEvent, IBdoConditionalEvent
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Condition script of this instance.
        /// </summary>
        [XmlElement("conditionScript")]
        public string ConditionScript { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConditionalEvent class.
        /// </summary>
        public BdoConditionalEvent() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ConditionalEvent class.
        /// </summary>
        /// <param name="conditionScript">The condition script of this instance.</param>
        /// <param name="kind">The kind of this instance.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="date">The date of this instance.</param>
        /// <param name="id">The ID of this instance.</param>
        public BdoConditionalEvent(
            String conditionScript,
            EventKinds kind,
            String title = "",
            BdoEventCriticality criticality = BdoEventCriticality.None,
            String description = "",
            DateTime? date = null,
            String id = null) : base(kind, title, criticality, description, date, id)
        {
            this.ConditionScript = conditionScript;
        }

        /// <summary>
        /// Instantiates a new instance of the ConditionalEvent class.
        /// </summary>
        /// <param name="aConditionScript">The condition script of this instance.</param>
        /// <param name="exception">The exception to consider.</param>
        /// <param name="criticality">The criticality to consider.</param>
        /// <param name="date">The date of this instance.</param>
        /// <param name="id">The ID of this instance.</param>
        public BdoConditionalEvent(
            String aConditionScript,
            Exception exception,
            BdoEventCriticality criticality = BdoEventCriticality.None,
            DateTime? date = null,
            String id = null)
            : base(exception, criticality, date, id)
        {
            this.ConditionScript = aConditionScript;
        }

        #endregion
    }
}
