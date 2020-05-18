namespace BindOpen.System.Assemblies
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
