using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Items.Routines.Definition.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoutineDefinitionDto : IAppExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DescribedDataItem> OutputResultCodes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSet ParameterStatement { get; set; }

    }
}