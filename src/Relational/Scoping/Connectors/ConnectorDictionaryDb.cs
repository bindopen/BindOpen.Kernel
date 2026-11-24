using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Scoping.Connectors
{
    /// <summary>
    /// This class represents a database entity connector dico.
    /// </summary>
    public class ConnectorDictionaryDb : TBdoExtensionDictionaryDb<ConnectorDefinitionDb>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [ForeignKey("ExtensionDictionaryId")]
        public List<ConnectorDefinitionDb> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConnectorDictionaryDb class.
        /// </summary>
        public ConnectorDictionaryDb() : base()
        {
        }

        #endregion
    }
}
