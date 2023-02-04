using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaData :
        IBdoItem,
        INamed, IReferenced,
        IIndexed
    {
        /// <summary>
        /// The kind of meta data of this instance.
        /// </summary>
        BdoMetaDataKind Kind { get; }

        /// <summary>
        /// The parent instance.
        /// </summary>
        IBdoMetaData Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IBdoMetaSpec> Specs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSpec GetSpec(string name = null);

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData WithSpecs(params IBdoMetaSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaSpec NewSpec();

        // Data

        /// <summary>
        /// 
        /// </summary>
        DataItemizationMode ItemizationMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes DataValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoReference DataReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoExpression DataExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}