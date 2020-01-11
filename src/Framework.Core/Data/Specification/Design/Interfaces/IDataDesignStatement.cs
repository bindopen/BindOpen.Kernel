using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Data.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataDesignStatement : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        DesignControlType ControlType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ControlWidth { get; set; }
    }
}