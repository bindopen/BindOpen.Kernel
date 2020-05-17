using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [XmlType("DataKeyValue", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "add.value", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DataKeyValue : MarshalByRefObject, IDataKeyValue
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
        public string Key { get; set; } = "en";

        /// <summary>
        /// Content of this instance.
        /// </summary>
        [DataMember(Name = "content")]
        [XmlText]
        public string Content { get; set; } = "";

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
        public DataKeyValue(string key, string aContent = null)
        {
            Key = key;
            Content = aContent;
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
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the text node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns></returns>
        public string GetTextNode(string nodeName, string indent)
        {
            string st = "";

            st += indent + nodeName + ":globalValue\n";
            st += "\t" + indent + nodeName + ":globalValue:key=\"" + Key + "\"\n";
            st += "\t" + indent + nodeName + ":globalValue:value=\"" + Content + "\"\n";
            return st;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public virtual Object Clone()
        {
            return MemberwiseClone() as DataKeyValue;
        }

        #endregion

        // --------------------------------------------------
        // IDisposable IMPLEMENTATION
        // --------------------------------------------------

        #region IDisposable Implementation

        /// <summary>
        /// Indicates whether this instance is disposed.
        /// </summary>
        private bool IsDisposed = false;

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (IsDisposed)
                return;

            // Free any unmanaged objects here.
            //
            IsDisposed = true;
        }

        #endregion
    }
}
