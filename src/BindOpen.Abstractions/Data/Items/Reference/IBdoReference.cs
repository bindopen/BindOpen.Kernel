using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoReference : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData SourceMetaData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData RootElement();

        /// <summary>
        /// 
        /// </summary>
        string DataHandlerUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaList PathDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object TargetObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoDatasource GetDatasource();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Get(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null);
    }
}