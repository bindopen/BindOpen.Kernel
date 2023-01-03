using BindOpen.Data.Items;
using System.Reflection;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a object element that is an element whose items are entities.
    /// </summary>
    public class ObjectElement :
        TBdoElement<IObjectElement, IObjectElementSpec, object>,
        IObjectElement
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ObjectElement class.
        /// </summary>
        public ObjectElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ObjectElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public ObjectElement(string name = null, string id = null)
            : base(name, "objectElem_", id)
        {
        }

        #endregion

        // --------------------------------------------------
        // IObjectElement Implementation
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        public string DefinitionUniqueId { get; set; }

        public IObjectElement WithDefinitionUniqueId(string definitionUniqueId)
        {
            DefinitionUniqueId = definitionUniqueId;

            return this;
        }

        /// <summary>
        /// The class full name of this instance.
        /// </summary>
        public string ClassFullName { get; set; }

        public IObjectElement WithClassFullName(string classFullName)
        {
            ClassFullName = classFullName;

            return this;
        }

        #endregion

        // --------------------------------------------------
        // ITBdoElement implementation
        // --------------------------------------------------

        #region Items

        // Specification ---------------------

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoElementSpec IBdoElement.NewSpecification()
        {
            return NewSpecification();
        }

        // Items ----------------------------

        /// <summary>
        /// Adds a new single item of this instance.
        /// </summary>
        /// <param name="objs">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public override IObjectElement WithItem(params object[] objs)
        {
            if (objs != null)
            {
                base.WithItem(objs);
                if (_item is BdoItem)
                {
                    var type = _item.GetType();
                    Assembly assembly = type.Assembly;
                    ClassFullName = type.FullName.ToString()
                        + (assembly == null ? string.Empty : "," + assembly.GetName().Name);
                }
            }

            return this;
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
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
            ObjectElement aObjectElement = base.Clone(areas) as ObjectElement;
            //if (DataSchemreference != null)
            //    aObjectElement.DataSchemreference = DataSchemreference.Clone() as DataHandler;

            return aObjectElement;
        }

        #endregion
    }
}
