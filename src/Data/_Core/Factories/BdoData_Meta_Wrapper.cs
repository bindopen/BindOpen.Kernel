using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Scoping;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This static class provides methods to create data metaent list.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaWrapper<T>(
            this IBdoScope scope,
            IBdoMetaSet detail = null,
            bool onlyMetaAttributes = true)
            where T : IBdoMetaWrapper, new()
        {
            var obj = new T()
                .WithScope(scope);

            if (detail != null)
            {
                obj.Detail.Update(detail);
                obj.UpdateFromMeta(detail, onlyMetaAttributes);
            }

            return obj;
        }
    }
}
