using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Definition;
using BindOpen.Framework.System.Diagnostics.Events;
using System.Xml.Serialization;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// This class represents a routine configuration.
    /// </summary>
    [XmlType("RoutineConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("routine", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoRoutineConfiguration
        : TBdoExtensionTitledItemConfiguration<BdoRoutineDefinition>, IBdoRoutineConfiguration
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
        public DataItemSet<BdoConditionalEvent> OutputEventSet { get; set; } = new DataItemSet<BdoConditionalEvent>();

        #endregion

        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        public BdoRoutineConfiguration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RoutineConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="outputEventSet">The output event set to consider.</param>
        /// <param name="items">The items to consider.</param>
        public BdoRoutineConfiguration(
            string definitionUniqueId,
            IDataItemSet<BdoConditionalEvent> outputEventSet = null,
            params IDataElement[] items)
            : base(BdoExtensionItemKind.Routine, definitionUniqueId, items)
        {
            //CommandSet = new DataItemSet<Command>(commandSet);
            outputEventSet = outputEventSet ?? new DataItemSet<BdoConditionalEvent>();
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
        public IDataItemSet<BdoConditionalEvent> NewOutputEventSet()
        {
            return this.OutputEventSet = this.OutputEventSet ?? new DataItemSet<BdoConditionalEvent>();
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
            IBdoRoutineConfiguration routine = base.Clone() as BdoRoutineConfiguration;
            //if (this.CommandSet != null)
            //    routine.CommandSet = this.CommandSet.Clone() as DataItemSet<Command>;
            if (this.OutputEventSet != null)
                routine.OutputEventSet = this.OutputEventSet.Clone() as DataItemSet<BdoConditionalEvent>;
            return routine;
        }

        #endregion
    }
}
