namespace System.Xml.Serialization
{
    /// <summary>
    /// This class represents a Xml array element.
    /// </summary>
    /// <remarks>This attribute is used for Json serialization.</remarks>
    public class XmlArrayElementAttribute : Attribute
    {
        public XmlArrayElementAttribute()
        {
        }

        public XmlArrayElementAttribute(string elementName)
        {
            ElementName = elementName;
        }

        public string ElementName { get; set; }
    }
}