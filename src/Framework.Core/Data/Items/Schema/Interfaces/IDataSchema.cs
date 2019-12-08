using BindOpen.Framework.Core.Data.Elements.Schema;
using BindOpen.Framework.Core.Data.References;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSchema : IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        DataReferenceDto MetaSchemreference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        SchemaZoneElement RootZone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentMetobject1"></param>
        /// <returns></returns>
        SchemaElement GetElementWithId(string id, SchemaElement parentMetobject1 = null);
    }
}