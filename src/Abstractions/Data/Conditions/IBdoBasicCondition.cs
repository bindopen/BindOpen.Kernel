using BindOpen.Data.Meta;

namespace BindOpen.Data.Conditions
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