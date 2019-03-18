using System;
using System.Collections;
using System.Collections.Generic;
using cor_base_wdl.business.dictionaries;
using cor_base_wdl.business.libraries;
using cor_base_wdl.data;
using cor_base_wdl.system.tracking;
using cor_runtime_wdl.business.libraries;
using cor_base_wdl.business.tasks;
using cor_base_wdl.system.script;
using cor_base_wdl.data.templates;
using cor_base_wdl.data.information;
using cor_base_wdl.data.database;
using cor_base_wdl.data.references;

namespace cor_runtime_wdl.business.universe
{

    /// <summary>
    /// This class represents a runtime dynamic business dictionary.
    /// </summary>
    public class RuntimeBusinessUniverse : BusinessUniverse
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Central business dictionary of this instance.
        /// </summary>
        protected RuntimeCentralBusinessDictionary myCentralBusinessDictionary = null;

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of RuntimeBusinessUniverse class.
        /// </summary>
        /// <param name="aCentralBusinessDictionary">Central business dictionary of this instance.</param>
        public RuntimeBusinessUniverse(
            AppDomain aAppDomain,
            RuntimeCentralBusinessDictionary aCentralBusinessDictionary)
        {
            this.myAppDomain = aAppDomain;
            this.myCentralBusinessDictionary = aCentralBusinessDictionary;
        }

        #endregion




    }
}
