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
        List<IBdoSpec> Specs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoSpec GetSpec(string name = null);

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData WithSpecs(params IBdoSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoSpec NewSpec();

        // Data

        /// <summary>
        /// 
        /// </summary>
        DataValueMode ValueMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes DataValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoExpression DataReference { get; set; }

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
            IBdoMetaList varSet = null,
            IBdoLog log = null);
    }
}