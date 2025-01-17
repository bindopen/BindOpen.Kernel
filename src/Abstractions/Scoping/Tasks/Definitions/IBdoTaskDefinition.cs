﻿using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Schema;

namespace BindOpen.Scoping.Tasks
{
    /// <summary>
    /// This interface defines a task definition.
    /// </summary>
    public interface IBdoTaskDefinition : IBdoExtensionDefinition, IBdoRuntimeTyped
    {
        /// <summary>
        /// The class reference.
        /// </summary>
        IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// The output schemas.
        /// </summary>
        ITBdoSet<IBdoSchema> Outputs { get; set; }

        /// <summary>
        /// The maximum index.
        /// </summary>
        float MaximumIndex { get; set; }

        /// <summary>
        /// Indicates whether the instance is executable.
        /// </summary>
        bool IsExecutable { get; set; }
    }
}