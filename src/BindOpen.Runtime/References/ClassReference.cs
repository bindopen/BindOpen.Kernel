using System.Xml.Serialization;

namespace BindOpen.Runtime.References
{
    /// <summary>
    /// This structure represents an class reference.
    /// </summary>
    public struct ClassReference
    {
        /// <summary>
        /// Library name.
        /// </summary>
        public string AssemblyName;

        /// <summary>
        /// Class name.
        /// </summary>
        public string ClassName;

        public override bool Equals(object obj)
        {
            var reference = obj as ClassReference?;
            if (reference == null) return false;

            return AssemblyName.Equals(reference.Value.AssemblyName) && ClassName.Equals(reference.Value.ClassName);
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
