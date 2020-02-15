using BindOpen.Data.Items;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents an titled data item.
    /// </summary>
    public interface ITitledDataItem : INamedDataItem, IGloballyTitled
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateBaseObject"></param>
        void Update(ITitledDataItem updateBaseObject);
    }
}
