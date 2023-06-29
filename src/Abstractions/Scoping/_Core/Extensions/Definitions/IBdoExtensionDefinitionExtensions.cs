namespace BindOpen.System.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoExtensionDefinitionExtensions
    {
        public static T WithPackage<T>(
            this T obj,
            IBdoPackageDefinition definition)
            where T : IBdoExtensionDefinition
        {
            if (obj != null)
            {
                obj.PackageDefinition = definition;
            }
            return obj;
        }

        public static T WithGroupId<T>(
            this T obj,
            string groupId)
            where T : IBdoExtensionDefinition
        {
            if (obj != null)
            {
                obj.GroupId = groupId;
            }
            return obj;
        }

        public static T WithImageUri<T>(
            this T obj,
            string imageUri)
            where T : IBdoExtensionDefinition
        {
            if (obj != null)
            {
                obj.ImageUri = imageUri;
            }
            return obj;
        }

        public static T AsEditable<T>(
            this T obj,
            bool isEditable)
            where T : IBdoExtensionDefinition
        {
            if (obj != null)
            {
                obj.IsEditable = isEditable;
            }
            return obj;
        }

        public static T AsIndexed<T>(
            this T obj,
            bool isIndexed)
            where T : IBdoExtensionDefinition
        {
            if (obj != null)
            {
                obj.IsIndexed = isIndexed;
            }
            return obj;
        }

        public static T WithLibraryId<T>(
            this T obj,
            string libraryId)
            where T : IBdoExtensionDefinition
        {
            if (obj != null)
            {
                obj.LibraryId = libraryId;
            }
            return obj;
        }
    }
}