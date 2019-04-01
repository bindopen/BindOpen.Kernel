using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes.Routines
{
    /// <summary>
    /// This class represents an routine index.
    /// </summary>
    [Serializable()]
    [XmlType("RoutineIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "routines.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class RoutineIndex : TAppExtensionItemIndex<IRoutineDefinition>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineIndex class.
        /// </summary>
        public RoutineIndex()
        {
        }

        #endregion        
    }
}
