using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Definition.Routines
{
    public interface IRoutineDefinition : IAppExtensionItemDefinition
    {
        List<ICommand> Commands { get; set; }

        string ItemClass { get; set; }

        List<IDescribedDataItem> OutputResultCodes { get; set; }

        IDataElementSet ParameterStatement { get; set; }
    }
}