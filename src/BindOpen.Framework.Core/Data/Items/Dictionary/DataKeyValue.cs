using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Items.Dictionary
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [Serializable()]
    [XmlType("DataKeyValue", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "add.value", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataKeyValue : MarshalByRefObject
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Key of this instance.
        /// </summary>
        [DataMember(Name = "key")]
        [XmlAttribute("key")]
        [DefaultValue("*")]
        public String Key { get; set; } = "en";

        /// <summary>
        /// Content of this instance.
        /// </summary>
        [DataMember(Name = "content")]
        [XmlText]
        public String Content { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of KeyValue class.
        /// </summary>
        public DataKeyValue()
        {
        }

        /// <summary>
        /// Instantiates a new instance of KeyValue class specifying
        /// its user interface language ID and its label.
        /// </summary>
        /// <param name="key">Key of this instance.</param>
        /// <param name="aContent">Content of this instance.</param>
        public DataKeyValue(String key, String aContent = null)
        {
            this.Key = key;
            this.Content = aContent;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Initializes the life time service.
        /// </summary>
        /// <returns>Null to remain the object's life forever.</returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        #endregion

        // --------------------------------------------------
        // EXPORTING
        // --------------------------------------------------

        #region Exporting

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the text node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns></returns>
        public String GetTextNode(String nodeName, String indent)
        {
            String st = "";

            st += indent + nodeName + ":globalValue\n";
            st += "\t" + indent + nodeName + ":globalValue:key=\"" + this.Key + "\"\n";
            st += "\t" + indent + nodeName + ":globalValue:value=\"" + this.Content + "\"\n";
            return st;
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public DataKeyValue Clone()
        {
            return new DataKeyValue()
            {
                Key = this.Key,
                Content = this.Content
            };
        }

        #endregion
    }
}
