using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents an titled data item.
    /// </summary>
    public interface ITitledDataItem : INamedDataItem, IGloballyTitled
    {
        void Update(ITitledDataItem updateBaseObject);
    }
}
