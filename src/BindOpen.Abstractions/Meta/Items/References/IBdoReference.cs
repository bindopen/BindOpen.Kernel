using BindOpen.Meta.Elements;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Meta.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoReference : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoMetaElement SourceElement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaElement RootElement();

        /// <summary>
        /// 
        /// </summary>
        object SourceObject { get; }

        /// <summary>
        /// 
        /// </summary>
        string DataHandlerUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoElementSet PathDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object TargetObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoDataSource GetDatasource();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Get(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);
    }
}