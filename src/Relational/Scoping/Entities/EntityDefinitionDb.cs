using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Scoping.Entities
{
    /// <summary>
    /// This class represents the entity definition database entity.
    /// </summary>
    public class EntityDefinitionDb : ExtensionDefinitionDb
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [ForeignKey("ItemClassId")]
        public ClassReferenceDb ItemClass { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string ItemClassId { get; set; }

        /// <summary>
        /// The viewer class of this instance.
        /// </summary>
        public string ViewerClass { get; set; }

        /// <summary>
        /// The outputs of this instance.
        /// </summary>
        [ForeignKey("MetaDataId")]
        public List<MetaDataDb> OutputSpecification { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityDefinitionDb class.
        /// </summary>
        public EntityDefinitionDb()
        {
        }

        #endregion
    }
}
