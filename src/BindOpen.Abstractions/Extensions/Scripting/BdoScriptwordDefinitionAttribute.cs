﻿using BindOpen.Data.Items;
using System;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a script word definition attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoScriptwordDefinitionAttribute : TitledDescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinitionAttribute class.
        /// </summary>
        public BdoScriptwordDefinitionAttribute() : base()
        {
        }

        #endregion
    }
}
