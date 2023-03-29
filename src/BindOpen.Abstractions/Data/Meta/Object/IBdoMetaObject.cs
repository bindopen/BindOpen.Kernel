using BindOpen.Data.Assemblies;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaObject : IBdoMetaSet
    {
        new void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        IBdoMetaObject WithData(object obj);

        /// <summary>
        /// 
        /// </summary>
        IBdoClassReference ClassReference { get; set; }
    }
}