namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICollectionElement : IDataElement, IDataElementSet
    {
        /// <summary>
        /// 
        /// </summary>
        new CollectionElementSpec Specification { get; set; }
    }
}