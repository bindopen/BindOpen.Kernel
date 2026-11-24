using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Entities
{
    /// <summary>
    /// This class represents a database entity entity dico.
    /// </summary>
    public class EntityDictionaryDb : TBdoExtensionDictionaryDb<EntityDefinitionDb>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [ForeignKey("ExtensionDefinitionId")]
        [JsonPropertyName("definitions")]
        [XmlArray("definitions")]
        [XmlArrayItem("add.definition")]
        public List<EntityDefinitionDb> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityDictionaryDb class.
        /// </summary>
        public EntityDictionaryDb()
        {
        }

        #endregion
    }
}
