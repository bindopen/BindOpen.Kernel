using System;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.System.Scripting
{
    /// <summary>
    /// This class represents a script item.
    /// </summary>
    public class ScriptItem : DataItem, INamedDataItem
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
        public ScriptItemKind Kind { get; set; } = ScriptItemKind.None;

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
            ScriptItemKind kind,
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
