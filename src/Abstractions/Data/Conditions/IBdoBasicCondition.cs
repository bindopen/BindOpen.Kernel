using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoBasicCondition : IBdoCondition
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData Argument1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData Argument2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataOperators Operator { get; set; }
    }
}