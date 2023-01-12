using BindOpen.Meta;
using BindOpen.Meta.Elements.Schema;
using BindOpen.Meta.Items;

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