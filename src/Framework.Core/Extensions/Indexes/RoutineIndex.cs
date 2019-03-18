using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents an routine index.
    /// </summary>
    [Serializable()]
    [XmlType("RoutineConfigurationIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "routines.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class RoutineConfigurationIndex : TAppExtensionItemIndex<RoutineDefinition>
    {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineConfigurationIndex class.
        /// </summary>
        public RoutineConfigurationIndex()
        {
        }

        #endregion        

    }
}
