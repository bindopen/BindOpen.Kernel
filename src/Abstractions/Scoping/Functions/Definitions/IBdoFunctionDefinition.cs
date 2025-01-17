﻿using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Schema;
using System;

namespace BindOpen.Scoping.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoFunctionDefinition : IBdoExtensionDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        ITBdoSet<IBdoSchema> AdditionalSchemas { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoDataType ParentDataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoDataType OutputDataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Type RuntimeClassType { get; set; }


        Delegate RuntimeFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoFunctionDomainedDelegate RuntimeScopedFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoFunctionDelegate RuntimeBasicFunction { get; set; }
    }
}