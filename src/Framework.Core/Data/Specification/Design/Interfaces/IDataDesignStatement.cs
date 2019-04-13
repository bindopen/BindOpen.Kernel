using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Specification.Design
{
    public interface IDataDesignStatement : IDataItem
    {
        DesignControlType ControlType { get; set; }
        string ControlWidth { get; set; }
    }
}