using System;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier.
    /// </summary>
    public abstract class TBdoCarrier<T> : BdoCarrier, ITBdoCarrier<T>
        where T : class, IBdoCarrier
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoCarrier class.
        /// </summary>
        protected TBdoCarrier() : base()
        {
        }

        #endregion

        // -----------------------------------------------
        // IBdoCarrier Implementation
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// Sets the path of this instance.
        /// </summary>
        /// <param name="path">The new path to consider. Null to update the existing one.</param>
        /// <param name="relativePath">The new relative path to consider. Null to keep the existing one.</param>
        /// <returns>Returns True if this instance exists. False otherwise.</returns>
        public new virtual T WithPath(string path = null, string relativePath = null)
        {
            return base.WithPath(path, relativePath) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public new T WithParentPath(string path)
        {
            return base.WithParentPath(path) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public new T WithCreationDate(DateTime? date)
        {
            return base.WithCreationDate(date) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public new T WithFlag(string flag)
        {
            return base.WithFlag(flag) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public new T AsReadonly(bool readOnly = false)
        {
            return base.AsReadonly(readOnly) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public new T WithLastAccessDate(DateTime? date)
        {
            return base.WithLastAccessDate(date) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public new T WithLastWriteDate(DateTime? date)
        {
            return base.WithLastWriteDate(date) as T;
        }

        #endregion
    }
}
