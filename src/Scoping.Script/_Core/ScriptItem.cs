using BindOpen.Kernel.Scoping.Script;
using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This class represents a script item.
    /// </summary>
    public class ScriptItem : BdoObject, IScriptItem
    {
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
        /// <param key="kind">The kind of the instance.</param>
        /// <param key="name">The name of the instance.</param>
        /// <param key="index">The index of the instance.</param>
        public ScriptItem(
            ScriptItemKinds kind,
            string name,
            int index)
        {
            Kind = kind;
            Name = name;
            Index = index;
        }

        #endregion

        // ------------------------------------------
        // IScriptItem Implementation
        // ------------------------------------------

        #region IScriptItem

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
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public IScriptItem WithName(string name)
        {
            Name = BdoData.NewName(name, "spec_");
            return this;
        }

        #endregion
    }
}
