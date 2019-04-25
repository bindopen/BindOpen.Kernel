using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definitions.Routines;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Extensions.Items.Routines
{
    /// <summary>
    /// This class represents a routine configuration.
    /// </summary>
    [Serializable()]
    [XmlType("RoutineConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("routine", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class RoutineConfiguration
        : TAppExtensionTitledItemConfiguration<RoutineDefinition>, IRoutineConfiguration
    {
        //------------------------------------------
        // PROPERTIES
        //-----------------------------------------

        #region Properties

        ///// <summary>
        ///// The command set of this instance.
        ///// </summary>
        //[XmlElement("commmandSet")]
        //public DataItemSet<Command> CommandSet { get; set; } = new DataItemSet<Command>();

        /// <summary>
        /// The output event set of this instance.
        /// </summary>
        [XmlElement("outputEventSet")]
        public DataItemSet<ConditionalEvent> OutputEventSet { get; set; } = new DataItemSet<ConditionalEvent>();

        #endregion

        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        public RoutineConfiguration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="commandSet">The command set to consider.</param>
        /// <param name="outputEventSet">The output event set to consider.</param>
        /// <param name="items">The items to consider.</param>
        public RoutineConfiguration(
            string definitionUniqueId,
            //IDataItemSet<Command> commandSet = null,
            IDataItemSet<ConditionalEvent> outputEventSet = null,
            params IDataElement[] items)
            : base(AppExtensionItemKind.Routine, definitionUniqueId, items)
        {
            //CommandSet = new DataItemSet<Command>(commandSet);
            outputEventSet = outputEventSet ?? new DataItemSet<ConditionalEvent>();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        ///// <summary>
        ///// Gets the command set of this instance and instantiates it if it is null.
        ///// </summary>
        ///// <returns>Returns the detail of this instance.</returns>
        //public IDataItemSet<Command> NewCommandSet()
        //{
        //    return this.CommandSet = this.CommandSet ?? new DataItemSet<ICommand>();
        //}

        /// <summary>
        /// Gets the output event set of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public IDataItemSet<ConditionalEvent> NewOutputEventSet()
        {
            return this.OutputEventSet = this.OutputEventSet ?? new DataItemSet<ConditionalEvent>();
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            IRoutineConfiguration routine = base.Clone() as RoutineConfiguration;
            //if (this.CommandSet != null)
            //    routine.CommandSet = this.CommandSet.Clone() as DataItemSet<Command>;
            if (this.OutputEventSet != null)
                routine.OutputEventSet = this.OutputEventSet.Clone() as DataItemSet<ConditionalEvent>;
            return routine;
        }

        #endregion
    }
}
