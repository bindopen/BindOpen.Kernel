//namespace BindOpen.Scoping
//{
//    /// <summary>
//    /// This static class provides extension methods for <see cref="IBdoExtension"/>.
//    /// </summary>
//    public static class IBdoExtensionExtensions
//    {
//        /// <summary>
//        /// Sets the extension kind (category) of this instance.
//        /// </summary>
//        /// <typeparam name="T">The type of the extension.</typeparam>
//        /// <param name="obj">The extension instance to consider.</param>
//        /// <param name="kind">The extension kind to set.</param>
//        /// <returns>Returns the specified extension instance for fluent chaining.</returns>
//        public static T WithDefinition<T>(
//            this T obj,
//            BdoExtensionKinds kind)
//            where T : IBdoExtension
//        {
//            if (obj != null)
//            {
//                obj.ExtensionKind = kind;
//            }
//            return obj;
//        }
//    }
//}