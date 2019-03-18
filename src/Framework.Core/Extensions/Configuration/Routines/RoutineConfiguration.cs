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
    public class RoutineConfiguration : TAppExtensionTitledItemConfiguration<RoutineDefinition>
    {
        //------------------------------------------
        // VARIABLES
        //-----------------------------------------

        #region Variables

        private DataElementSet _ParameterDetail = new DataElementSet();
        private DataItemSet<Command> _CommandSet = new DataItemSet<Command>();

        private DataItemSet<ConditionalEvent> _OutputEventSet = new DataItemSet<ConditionalEvent>();

        #endregion


        //------------------------------------------
        // PROPERTIES
        //-----------------------------------------

        #region Properties

        /// <summary>
        /// The parameter detail of this instance.
        /// </summary>
        [XmlElement("parameterDetail")]
        public DataElementSet ParameterDetail
        {
            get { return this._ParameterDetail; }
            set { this._ParameterDetail = value; }
        }

        /// <summary>
        /// The command set of this instance.
        /// </summary>
        [XmlElement("commmandSet")]
        public DataItemSet<Command> CommandSet
        {
            get { return this._CommandSet; }
            set { this._CommandSet = value; }
        }

        /// <summary>
        /// The output event set of this instance.
        /// </summary>
        [XmlElement("outputEventSet")]
        public DataItemSet<ConditionalEvent> OutputEventSet
        {
            get
            {
                return this._OutputEventSet;
            }
            set { this._OutputEventSet = value; }
        }

        #endregion


        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        public RoutineConfiguration()
            : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The defintion unique name to consider.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        /// <param name="commandSet">The command set to consider.</param>
        /// <param name="outputEventSet">The output event set to consider.</param>
        public RoutineConfiguration(
            String name,
            String definitionName,
            DataElementSet parameterDetail = null,
            DataItemSet<Command> commandSet = null,
            DataItemSet<ConditionalEvent> outputEventSet = null)
            : base(name, definitionName, null, "routine_")
        {
            this.DefinitionUniqueId = definitionName;
            this._ParameterDetail = parameterDetail;
            this._CommandSet = (commandSet ?? new DataItemSet<Command>());
            this._OutputEventSet = (outputEventSet ?? new DataItemSet<ConditionalEvent>());
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
        public DataElementSet NewParameterDetail()
        {
            return this._ParameterDetail = this._ParameterDetail ?? new DataElementSet();
        }

        /// <summary>
        /// Gets the command set of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public DataItemSet<Command> NewCommandSet()
        {
            return this._CommandSet = this._CommandSet ?? new DataItemSet<Command>();
        }

        /// <summary>
        /// Gets the output event set of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public DataItemSet<ConditionalEvent> NewOutputEventSet()
        {
            return this._OutputEventSet = this._OutputEventSet ?? new DataItemSet<ConditionalEvent>();
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
        public override Object Clone()
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
