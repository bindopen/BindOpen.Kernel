namespace BindOpen.Framework.Core.Data.Elements.Scalar
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScalarElement : IDataElement
    {
        /// <summary>
        /// 
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        new ScalarElementSpec Specification { get; set; }
    }
}