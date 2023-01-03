using BindOpen.Data.Items;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICollectionElement :
        ITBdoElement<ICollectionElement, ICollectionElementSpec, IBdoElement>,
        ITBdoItemSet<IBdoElement>
    {
    }
}