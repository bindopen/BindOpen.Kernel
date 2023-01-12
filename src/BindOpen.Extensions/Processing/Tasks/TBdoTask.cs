using BindOpen.Meta;
using BindOpen.Meta.Elements;
using BindOpen.Runtime.Scopes;
using BindOpen.Logging;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents an task.
    /// </summary>
    public abstract class TBdoTask<T> : BdoTask, ITBdoTask<T>
        where T : class, IBdoTask
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoTask class.
        /// </summary>
        protected TBdoTask() : base()
        {
        }

        #endregion

        //------------------------------------------
        // TBdoTask Implementation
        //-----------------------------------------

        #region TBdoTask

        /// <summary>
        /// Updates the relative paths of this instance.
        /// </summary>
        /// <param name="relativePath">The relative path to consider.</param>
        public new T UpdateAbsolutePaths(string relativePath)
        {
            //foreach (BdoElement currentElement in _Inputs)
            //    if (currentElement.CarrierKind == DocumentKind.RepositoryFile)
            //    {
            //        RepositoryFile aRepositoryFile = (RepositoryFile)currentElement.GetValue();
            //        if (aRepositoryFile != null)
            //        {
            //            aRepositoryFile.Path = RepositoryFile.GetAbsolutePath(aRepositoryFile.Path, relativePath);
            //            aRepositoryFile.Paths = RepositoryFile.GetAbsolutePath(aRepositoryFile.Paths, relativePath);
            //            currentElement.SetValue(aRepositoryFile);
            //        }
            //    }
            //foreach (BdoElement currentElement in _Outputs)
            //    if (currentElement.CarrierKind == DocumentKind.RepositoryFile)
            //    {
            //        RepositoryFile aRepositoryFile = (RepositoryFile)currentElement.GetValue();
            //        if (aRepositoryFile != null)
            //        {
            //            aRepositoryFile.Path = RepositoryFile.GetAbsolutePath(aRepositoryFile.Path, relativePath);
            //            aRepositoryFile.Paths = RepositoryFile.GetAbsolutePath(aRepositoryFile.Paths, relativePath);
            //            currentElement.SetValue(aRepositoryFile);
            //        }
            //    }
            return this as T;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use for execution.</param>
        /// <param name="runtimeMode">The runtime mode to consider.</param>
        /// <returns>Returns the output value of the execution.</returns>
        public new virtual T Execute(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null)
        {
            return base.Execute(scope, varElementSet, runtimeMode, log) as T;
        }

        #endregion
    }
}