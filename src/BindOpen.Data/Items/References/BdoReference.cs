using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data reference.
    /// </summary>
    public class BdoReference : BdoItem, IBdoReference
    {
        private const int __MaxLevel = 255;

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Source element of this instance.
        /// </summary>
        public IBdoMetaData Source { get; set; }

        /// <summary>
        /// The data handler unique name of this instance.
        /// </summary>
        public string DataHandlerUniqueName { get; set; }

        /// <summary>
        /// The path detail of this instance.
        /// </summary>
        public IBdoMetaList PathDetail { get; set; }

        /// <summary>
        /// Target item of this instance.
        /// </summary>
        public object TargetObject { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        public BdoReference()
            : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// The root element of this instance.
        /// </summary>
        public IBdoMetaData Root() => Root(-1);

        private IBdoMetaData Root(int current = -1, int max = __MaxLevel)
        {
            if (current <= max)
            {
                return Source?.Reference == null ? Source : Root(current + 1);
            }
            return null;
        }

        private void BuildLineage(
            ref List<IBdoMetaData> tree,
            int current = -1,
            int max = __MaxLevel)
        {
            if (tree != null && current <= max)
            {
                if (Source == null)
                {
                    return;
                }
                tree.Add(Source);
                BuildLineage(ref tree, current, max);
            }
            tree = null;
            return;
        }

        /// <summary>
        /// Gets the items from the source of this instance and update the target items.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the retrieved items.</returns>
        public object Get(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            if (scope == null)
            {
                log?.AddError("Scope missing");
                return null;
            }

            var line = new List<IBdoMetaData>();
            BuildLineage(ref line);

            if (line == null)
            {
                log?.AddError("Circular source refernce found");
                return null;
            }

            line?.Reverse();

            object obj = null;
            foreach (var meta in line)
            {
                obj = scope.GetData(
                    DataHandlerUniqueName,
                    obj, PathDetail,
                    varSet, log);
            }

            return obj;
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
            BdoReference dataReference = Clone<BdoReference>(areas);
            dataReference.Source = Source.Clone<IBdoMetaData>();
            dataReference.PathDetail = PathDetail.Clone<BdoMetaList>();

            return dataReference;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}