using BindOpen.MetaData.Items;

namespace BindOpen.MetaData.Elements
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