﻿using System.Collections.Generic;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a data reference.
/// </summary>
public class BdoAssemblyReferenceCollection : List<IBdoAssemblyReference>, IBdoAssemblyReferenceCollection
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the BdoAssemblyReferenceCollection class.
    /// </summary>
    /// <param key="references">The references to consider.</param>
    public BdoAssemblyReferenceCollection(IEnumerable<IBdoAssemblyReference> references = null)
    {
        if (references != null)
        {
            foreach (var reference in references)
            {
                Add(reference);
            }
        }
    }

    #endregion
}