using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Items.Routines.Definition.Dto
{
    public interface IRoutineDefinitionDto : IAppExtensionItemDefinitionDto
    {
        string ItemClass { get; set; }
        //List<Command> Commands { get; set; }
        List<DescribedDataItem> OutputResultCodes { get; set; }
        DataElementSet ParameterStatement { get; set; }

    }
}