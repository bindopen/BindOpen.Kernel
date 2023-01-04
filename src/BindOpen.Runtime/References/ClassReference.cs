namespace BindOpen.Runtime.References
{
    /// <summary>
    /// This structure represents an class reference.
    /// </summary>
    public class ClassReference
    {
        /// <summary>
        /// Library name.
        /// </summary>
        public readonly string AssemblyName;

        /// <summary>
        /// Class name.
        /// </summary>
        public readonly string ClassName;

        public ClassReference(string assemblyName, string className)
        {
            AssemblyName = assemblyName;
            ClassName = className;
        }

        public override bool Equals(object obj)
        {
            var reference = obj as ClassReference;
            if (reference == null) return false;

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
