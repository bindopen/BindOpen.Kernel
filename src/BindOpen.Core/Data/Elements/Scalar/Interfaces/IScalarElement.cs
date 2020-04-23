namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScalarElement : IDataElement
    {
        /// <summary>
        /// 
        /// </summary>
        string DtoValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        new ScalarElementSpec Specification { get; set; }
    }
}