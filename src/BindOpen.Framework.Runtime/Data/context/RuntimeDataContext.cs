using System;

namespace cor_runtime_wdl.data.context
{

    /// <summary>
    /// This class represents a runtime data context. A runtime data context can consider runtime extension handler.
    /// </summary>
    [Serializable()]
    public class RuntimeDataContext : DataContext
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private RuntimeExtensionHandler myRuntimeExtensionHandler = null;

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of RuntimeDataContext class.
        /// </summary>
        public RuntimeDataContext() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of RuntimeDataContext class specifying a central extension handler and the IDs of the library to load.
        /// </summary>
        /// <param name="aRuntimeExtensionHandler">The runtime dynamic extension handler to consider.</param>
        public RuntimeDataContext(RuntimeExtensionHandler aRuntimeExtensionHandler)
        {
            this.myRuntimeExtensionHandler = aRuntimeExtensionHandler;
            this.Load();
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        // LOAD ------------------------------------

        /// <summary>
        /// Loads the business data contexts.
        /// </summary>
        public void Load()
        {
            if (this.myRuntimeExtensionHandler != null)
                foreach (BusinessLibrary aDynamicBusinessLibrary in this.myRuntimeExtensionHandler.GetLibraries())
                    try
                    {
                        String aDataContextTypeName =
                            aDynamicBusinessLibrary.AssemblyName +
                            ".definition.context.DataContext_" + aDynamicBusinessLibrary.Name.ToLower();
                        BusinessDataContext aBusinessDataContext = 
                            this.myRuntimeExtensionHandler.CreateDataContext(aDynamicBusinessLibrary.Name, this);
                    }
                    catch
                    {
                    }
        }

        #endregion

    }
}
