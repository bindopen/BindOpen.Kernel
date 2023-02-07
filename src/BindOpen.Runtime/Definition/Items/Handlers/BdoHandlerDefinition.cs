using BindOpen.Data.Meta;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a handler definition.
    /// </summary>
    public class BdoHandlerDefinition : BdoExtensionItemDefinition, IBdoHandlerDefinition
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The calling class of this instance.
        /// </summary>
        public string CallingClass { get; set; }

        /// <summary>
        /// Name of the GET function.
        /// </summary>
        public string GetFunctionName { get; set; } = "Get";

        /// <summary>
        /// The parameter specification of this instance.
        /// </summary>
        public IBdoSpecList ParameterSpecification { get; set; } = new BdoSpecList();

        /// <summary>
        /// Name of the POST function.
        /// </summary>
        public string PostFunctionName { get; set; } = "Post";

        /// <summary>
        /// Runtime GET function of this instance.
        /// </summary>
        public BdoHandlerGetFunction RuntimeFunctionGet { get; set; }

        /// <summary>
        /// Runtime POST function of this instance.
        /// </summary>
        public BdoHandlerPostFunction RuntimeFunctionPost { get; set; }

        /// <summary>
        /// The parameter specification of this instance.
        /// </summary>
        public IBdoSpec SourceSpecification { get; set; }

        /// <summary>
        /// The target specification of this instance.
        /// </summary>
        public IBdoSpec TargetSpecification { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public new string UniqueId { get => ExtensionDefinition?.UniqueId + "$" + Name; set { } }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="extensionDefinition">The extensition definition to consider.</param>
        public BdoHandlerDefinition(
            string name,
            IBdoExtensionDefinition extensionDefinition) : base(name, "handlerDef_", extensionDefinition)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public override string Key()
        {
            return UniqueId;
        }

        #endregion
    }
}
