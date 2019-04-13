using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Routines;

namespace BindOpen.Framework.Core.Extensions.Indexes.Routines
{
    /// <summary>
    /// This class represents an routine index.
    /// </summary>
    [Serializable()]
    [XmlType("RoutineIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "routines.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class RoutineIndexDto : TAppExtensionItemIndexDto<IRoutineDefinitionDto>
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
