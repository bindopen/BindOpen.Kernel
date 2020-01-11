using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Data.Items
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
