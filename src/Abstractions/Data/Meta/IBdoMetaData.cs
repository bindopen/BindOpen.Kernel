using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System.Collections.Generic;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaData :
        IBdoObjectNotMetable, IBdoReferenced, IBdoConditional,
        INamed, IReferenced, IIndexed, IBdoDataTyped,
        ITChild<IBdoMetaData>, IBdoSpecified,
        IUpdatable
    {
        /// <summary>
        /// The kind of meta data of this instance.
        /// </summary>
        BdoMetaDataKind MetaDataKind { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="metaSet"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        object GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="metaSet"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        T GetData<T>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="metaSet"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        IList<object> GetDataList(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="metaSet"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        IList<T> GetDataList<T>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        IBdoSpecRule GetSpecRule(
            string groupId,
            BdoSpecRuleKinds ruleKind = BdoSpecRuleKinds.Requirement,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        void SetData(object obj);
    }
}