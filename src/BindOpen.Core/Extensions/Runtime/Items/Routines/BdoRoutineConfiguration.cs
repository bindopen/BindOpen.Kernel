using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics.Events;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
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
        public TDataItemSet<BdoConditionalEvent> OutputEventSet { get; set; } = new TDataItemSet<BdoConditionalEvent>();

        #endregion

        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoRoutineConfiguration class.
        /// </summary>
        public BdoRoutineConfiguration() : base(BdoExtensionItemKind.Routine, null)
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoRoutineConfiguration Add(params IDataElement[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoRoutineConfiguration WithItems(params IDataElement[] items)
        {
            base.WithItems(items);
            return this;
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
        public ITDataItemSet<BdoConditionalEvent> NewOutputEventSet()
        {
            return this.OutputEventSet = this.OutputEventSet ?? new TDataItemSet<BdoConditionalEvent>();
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
        public override object Clone(params string[] areas)
        {
            IBdoRoutineConfiguration routine = base.Clone(areas) as BdoRoutineConfiguration;
            //if (this.CommandSet != null)
            //    routine.CommandSet = this.CommandSet.Clone() as DataItemSet<Command>;
            if (this.OutputEventSet != null)
                routine.OutputEventSet = this.OutputEventSet.Clone() as TDataItemSet<BdoConditionalEvent>;
            return routine;
        }

        #endregion
    }
}
