using System.Xml.Serialization;

namespace BindOpen.System.Assemblies
{
    /// <summary>
    /// This structure represents an class reference.
    /// </summary>
    [XmlType("ClassReference", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "class.reference", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public struct ClassReference
    {
        /// <summary>
        /// Library name.
        /// </summary>
        [XmlElement("assembly")]
        public string AssemblyName;

        /// <summary>
        /// Class name.
        /// </summary>
        [XmlElement("class")]
        public string ClassName;

        public override bool Equals(object obj)
        {
            if (obj == null) return this == null;

            var reference = (ClassReference)obj;
            return AssemblyName.Equals(reference.AssemblyName) && ClassName.Equals(reference.ClassName);
        }

        public override int GetHashCode()
        {
            return AssemblyName?.GetHashCode() ?? 0 + ClassName?.GetHashCode() ?? 0;
        }

        public static bool operator ==(ClassReference left, ClassReference right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ClassReference left, ClassReference right)
        {
            return !(left == right);
        }
    }
}
