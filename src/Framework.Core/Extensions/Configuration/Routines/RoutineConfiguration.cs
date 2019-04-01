using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Extensions.Configuration.Routines
{
    /// <summary>
    /// This class represents a routine configuration.
    /// </summary>
    [Serializable()]
    [XmlType("RoutineConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("routine", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class RoutineConfiguration : TAppExtensionTitledItemConfiguration<IRoutineDefinition>, IRoutineConfiguration
    {
        //------------------------------------------
        // PROPERTIES
        //-----------------------------------------

        #region Properties

        /// <summary>
        /// The parameter detail of this instance.
        /// </summary>
        [XmlElement("parameterDetail")]
        public IDataElementSet ParameterDetail { get; set; } = new DataElementSet();

        /// <summary>
        /// The command set of this instance.
        /// </summary>
        [XmlElement("commmandSet")]
        public IDataItemSet<ICommand> CommandSet { get; set; } = new DataItemSet<ICommand>();

        /// <summary>
        /// The output event set of this instance.
        /// </summary>
        [XmlElement("outputEventSet")]
        public IDataItemSet<IConditionalEvent> OutputEventSet { get; set; } = new DataItemSet<IConditionalEvent>();

        #endregion

        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        public RoutineConfiguration()
            : base(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        /// <param name="commandSet">The command set to consider.</param>
        /// <param name="outputEventSet">The output event set to consider.</param>
        protected RoutineConfiguration(
            string name,
            IRoutineDefinition definition = default,
            string namePreffix = "task_",
            IDataElementSet parameterDetail = null,
            IDataItemSet<ICommand> commandSet = null,
            IDataItemSet<IConditionalEvent> outputEventSet = null)
            : this(name, definition?.Key(), namePreffix, parameterDetail, commandSet, outputEventSet)
        {
            _definition = definition;
        }

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        /// <param name="commandSet">The command set to consider.</param>
        /// <param name="outputEventSet">The output event set to consider.</param>
        protected RoutineConfiguration(
            string name,
            string definitionUniqueId,
            string namePreffix = "task_",
            IDataElementSet parameterDetail = null,
            IDataItemSet<ICommand> commandSet = null,
            IDataItemSet<IConditionalEvent> outputEventSet = null)
            : base(name, definitionUniqueId, namePreffix)
        {
            DefinitionUniqueId = definitionUniqueId;
            ParameterDetail = parameterDetail;
            CommandSet = commandSet ?? new DataItemSet<ICommand>();
            OutputEventSet = outputEventSet ?? new DataItemSet<IConditionalEvent>();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the detail of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public IDataElementSet NewParameterDetail()
        {
            return this.ParameterDetail = this.ParameterDetail ?? new DataElementSet();
        }

        /// <summary>
        /// Gets the command set of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public IDataItemSet<ICommand> NewCommandSet()
        {
            return this.CommandSet = this.CommandSet ?? new DataItemSet<ICommand>();
        }

        /// <summary>
        /// Gets the output event set of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public IDataItemSet<IConditionalEvent> NewOutputEventSet()
        {
            return this.OutputEventSet = this.OutputEventSet ?? new DataItemSet<IConditionalEvent>();
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
            RoutineConfiguration routine = base.Clone() as RoutineConfiguration;
            if (this.CommandSet != null)
                routine.CommandSet = this.CommandSet.Clone() as DataItemSet<Command>;
            if (this.OutputEventSet != null)
                routine.OutputEventSet = this.OutputEventSet.Clone() as DataItemSet<ConditionalEvent>;
            return routine;
        }

        #endregion
    }
}
