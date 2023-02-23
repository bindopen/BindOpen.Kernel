using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using BindOpen.Data.Meta.Reflection;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension item config.
    /// </summary>
    public abstract class BdoExtension : BdoItem, IBdoExtension
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtension class.
        /// </summary>
        protected BdoExtension() : base()
        {
        }

        #endregion

        // -----------------------------------------------
        // IBdoExtension
        // -----------------------------------------------

        #region IBdoExtension

        /// <summary>
        /// The config of this instance.
        /// </summary>
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IBdoConfiguration ToConfig()
        {
            var config = this.ToMetaData<IBdoConfiguration>();
            config.DefinitionUniqueName = DefinitionUniqueName;
            return config;
        }

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion
    }
}
