using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Specification.Design
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