using BindOpen.Framework.Data.Conditions;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.System.Diagnostics.Events;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Application.Messages
{
    /// <summary>
    /// This structure respresents a display message.
    /// </summary>
    public class DisplayMessage : StoredDataItem
    {
        // ------------------------------------------
        // ENUMERATIONS
        // ------------------------------------------

        #region Enumerations

        /// <summary>
        /// This enumeration lists all the possible display message options.
        /// </summary>
        public enum DisplayMessageOption
        {
            /// <summary>
            /// The automatic message cannot be hidden.
            /// </summary>
            CannotBeHidden,

            /// <summary>
            /// The automatic message can be hidden.
            /// </summary>
            CanBeHidden,

            /// <summary>
            /// The automatic message can be hidden forever.
            /// </summary>
            CanBeHiddenForEver
        }

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DisplayMessageOption _DisplayOption = DisplayMessageOption.CanBeHidden;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Display condition of this instance.
        /// </summary>
        [XmlElement("displayCondition")]
        public ScriptCondition DisplayCondition
        {
            set;
            get;
        }

        /// <summary>
        /// Displayed label of this instance.
        /// </summary>
        [XmlElement("displayedLabel")]
        public DictionaryDataItem DisplayedLabel
        {
            set;
            get;
        }

        /// <summary>
        /// Link URI of this instance.
        /// </summary>
        [XmlElement("linkUri")]
        public string LinkUri
        {
            set;
            get;
        }

        /// <summary>
        /// Display option of this instance.
        /// </summary>
        [XmlElement("displayOption")]
        public DisplayMessageOption DisplayOption
        {
            get
            {
                return this._DisplayOption;
            }
            set
            {
                this._DisplayOption = value;
            }
        }

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        [XmlElement("kind")]
        public EventKinds Kind
        {
            set;
            get;
        }

        /// <summary>
        /// Indicates whether this instance can be hidden.
        /// </summary>
        [XmlElement("canBeHidden")]
        public Boolean CanBeHidden
        {
            set;
            get;
        }

        /// <summary>
        /// Indicates whether the redirection of this instance is automatic.
        /// </summary>
        [XmlElement("isAutomaticRedirection")]
        public Boolean IsAutomaticRedirection
        {
            set;
            get;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Creates a new instance of the DisplayMessage class.
        /// </summary>
        public DisplayMessage()
        {
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public override object Clone()
        {
            DisplayMessage aDisplayMessage = base.Clone() as DisplayMessage;
            aDisplayMessage.DisplayCondition = this.DisplayCondition.Clone() as ScriptCondition;
            aDisplayMessage.DisplayedLabel = this.DisplayedLabel.Clone() as DictionaryDataItem;
            return aDisplayMessage;
        }

        #endregion
    }
}