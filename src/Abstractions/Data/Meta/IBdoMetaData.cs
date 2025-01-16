using BindOpen.Data.Assemblies;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaData :
        IBdoObjectNotMetable, IBdoReferenced, IBdoConditional, IBdoScoped, IIdentified,
        INamed, IReferenced, IIndexed, IBdoDataTyped,
        ITChild<IBdoMetaData>, IBdoSchematized,
        IUpdatable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="metaSet"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        object GetData(
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="metaSet"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        object GetData(
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
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        T GetData<T>(
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
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        IList<object> GetDataList(
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
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        IList<T> GetDataList<T>(
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        IBdoSchemaRule GetSchemaRule(
            IBdoScope scope,
            string groupId,
            BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        IBdoSchemaRule GetSchemaRule(
            string groupId,
            BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        bool GetConditionValue(
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        void SetData(object obj);
    }
}