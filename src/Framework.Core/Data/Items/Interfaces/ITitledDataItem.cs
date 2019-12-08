using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Items
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
