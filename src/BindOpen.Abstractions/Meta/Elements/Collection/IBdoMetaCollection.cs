using BindOpen.Meta.Items;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaCollection :
        ITBdoMetaElement<IBdoMetaCollection, IBdoMetaCollectionSpec, IBdoMetaElement>,
        ITBdoItemSet<IBdoMetaElement>
    {
    }
}