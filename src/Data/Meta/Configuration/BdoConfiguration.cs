using BindOpen.Kernel.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a config.
    /// </summary>
    public partial class BdoConfiguration : BdoMetaSet, IBdoConfiguration
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        public BdoConfiguration() : base()
        {
        }

        #endregion

        // -------------------------------------------------------
        // IBdoConfiguration Implementation
        // -------------------------------------------------------

        #region IBdoConfiguration

        /// <summary>
        /// The using file paths of this instance.
        /// </summary>
        public IList<string> UsedItemIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override TChild Descendant<TChild>(
            params object[] tokens)
        {
            var token = tokens?.FirstOrDefault();

            object child = default;

            if (token?.ToString().StartsWith('^') == true)
            {
                var tokenSt = token?.ToString().ToSubstring(1);

                if (tokenSt?.StartsWith(':') == true)
                {
                    object tokenInt = tokenSt.ToSubstring(1).ToObject(DataValueTypes.Integer);
                    if (tokenInt is int index)
                    {
                        child = _Children[index];
                    }
                }
                else if (token is string key)
                {
                    child = this.Child(q => q.BdoKeyEquals(tokenSt), false);
                }

                if (tokens?.Length == 1) return child.As<TChild>();

                if (child is IBdoSet childSet)
                {
                    tokens = tokens?.Skip(1).ToArray();
                    return childSet.Descendant<TChild>(tokens);
                }
                else
                    return default;
            }

            return base.Descendant<TChild>(tokens);
        }

        #endregion

        // ------------------------------------------
        // IIndexed Implementation
        // ------------------------------------------

        #region IIndexed

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

        #endregion

        // ------------------------------------------
        // IStorable Implementation
        // ------------------------------------------

        #region IStorable

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        public DateTime? LastModificationDate { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Title { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Description { get; set; }

        #endregion

        // ------------------------------------------
        // ITTreeNode Implementation
        // ------------------------------------------

        #region ITParent

        public IBdoConfiguration Parent { get; set; }

        protected ITBdoSet<IBdoConfiguration> _children = null;

        public ITBdoSet<IBdoConfiguration> _Children { get => _children; set { _children = value; } }

        public Q InsertChild<Q>(Action<Q> updater) where Q : IBdoConfiguration, new()
        {
            var child = BdoData.NewConfig<Q>();
            updater?.Invoke(child);

            child.WithParent<IBdoConfiguration, IBdoConfiguration>(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
        {
            _children?.Remove(filter);
        }

        #endregion
    }
}
