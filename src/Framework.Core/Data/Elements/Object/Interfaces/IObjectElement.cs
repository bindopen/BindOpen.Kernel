using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;

namespace BindOpen.Framework.Core.Data.Elements._Object
{
    public interface IObjectElement : IDataElement
    {
        string ClassFullName { get; set; }

        string DefinitionUniqueId { get; set; }

        new List<DataElementSet> Objects { get; set; }

        new ObjectElementSpec Specification { get; set; }
    }
}