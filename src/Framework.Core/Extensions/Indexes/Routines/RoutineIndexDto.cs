using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Routines;
using BindOpen.Framework.Core.Extensions.Items.Routines.Definition;

namespace BindOpen.Framework.Core.Extensions.Indexes.Routines
{
    /// <summary>
    /// This class represents an routine index.
    /// </summary>
    [Serializable()]
    [XmlType("RoutineIndex", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "routines.index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class RoutineIndexDto : TAppExtensionItemIndexDto<RoutineDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineIndex class.
        /// </summary>
        public RoutineIndexDto()
        {
        }

        #endregion        
    }
}
