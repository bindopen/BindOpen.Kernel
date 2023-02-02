using BindOpen.Data.Items;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaItem :
        IBdoItem,
        INamed, IReferenced,
        IIndexed
    {
        /// <summary>
        /// The kind of meta data of this instance.
        /// </summary>
        BdoMetaDataKind Kind { get; }

        /// <summary>
        /// The parent instance.
        /// </summary>
        IBdoMetaItem Parent { get; set; }
    }
}