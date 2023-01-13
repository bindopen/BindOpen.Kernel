using BindOpen.MetaData;
using BindOpen.MetaData.Elements.Schema;
using BindOpen.MetaData.Items;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSchema :
        ITIdentifiedPoco<IDataSchema>,
        ITNamedPoco<IDataSchema>,
        ITGloballyTitledPoco<IDataSchema>,
        ITGloballyDescribedPoco<IDataSchema>
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoReference MetaSchemaReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSchemaZone RootZone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentMetobject1"></param>
        /// <returns></returns>
        IBdoMetaSchema GetElementWithId(string id, IBdoMetaSchema parentMetobject1 = null);
    }
}