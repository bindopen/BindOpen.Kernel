using BindOpen.Data.Items;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IBdoMetaSet :
        ITBdoMetaData<IBdoMetaSet, IBdoMetaSetSpec, IBdoMetaData>,
        ITBdoItemSet<IBdoMetaData>
    {
        new IBdoMetaSet WithItems(
            params IBdoMetaData[] items);
    }
}