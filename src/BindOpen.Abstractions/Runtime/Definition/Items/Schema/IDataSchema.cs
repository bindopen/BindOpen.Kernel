using BindOpen.Data;
using BindOpen.Data.Elements.Schema;
using BindOpen.Data.Items;

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
        ISchemaZoneElement RootZone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentMetobject1"></param>
        /// <returns></returns>
        ISchemaElement GetElementWithId(string id, ISchemaElement parentMetobject1 = null);
    }
}