using BindOpen.Data.Assemblies;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents the entity definition.
    /// </summary>
    public class BdoEntityDefinition : BdoExtensionDefinition,
        IBdoEntityDefinition
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// The set of detail specifications of this instance.
        /// </summary>
        public ITBdoSet<IBdoSpec> SpecDetail { get; set; } = new TBdoSet<IBdoSpec>();

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary ViewerDictionary { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        public BdoEntityDefinition(
            string name,
            IBdoPackageDefinition extensionDefinition,
            string preffix = "entityDef_")
            : base(name, preffix, extensionDefinition)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public override string Key()
        {
            return UniqueName;
        }

        #endregion
    }
}
