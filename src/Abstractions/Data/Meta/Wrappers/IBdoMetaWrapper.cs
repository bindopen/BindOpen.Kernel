using BindOpen.System.Scoping;

namespace BindOpen.System.Data.Meta
{
    public interface IBdoMetaWrapper : IBdoObject, IBdoScoped, IBdoDetailed
    {
        T GetData<T>(string name);

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        object GetData(string name);

        void UpdateDetail(
            IBdoMetaSet detail,
            bool onlyMetaAttributes = true);

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="propertyName">The calling property name to consider.</param>
        void UpdateProperties(
            bool onlyMetaAttributes = true);
    }
}