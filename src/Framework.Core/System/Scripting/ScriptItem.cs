using BindOpen.Framework.Data.Items;
using System;

namespace BindOpen.Framework.System.Scripting
{
    /// <summary>
    /// This class represents a script item.
    /// </summary>
    public class ScriptItem : DataItem, INamed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int Index { get; set; } = -1;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptItem class.
        /// </summary>
        public ScriptItem()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptItem class.
        /// </summary>
        /// <param name="kind">The kind of the instance.</param>
        /// <param name="name">The name of the instance.</param>
        /// <param name="index">The index of the instance.</param>
        public ScriptItem(
            ScriptItemKinds kind,
            String name,
            int index)
        {
            this.Kind = kind;
            this.Name = name;
            this.Index = index;
        }

        #endregion
    }
}
