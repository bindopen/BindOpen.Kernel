namespace System.Xml.Serialization
{
    /// <summary>
    /// This class represents a Xml array element.
    /// </summary>
    /// <remarks>This attribute is used for Json serialization.</remarks>
    public class XmlArrayElementAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public XmlArrayElementAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        public XmlArrayElementAttribute(string elementName)
        {
            ElementName = elementName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ElementName { get; set; }
    }
}