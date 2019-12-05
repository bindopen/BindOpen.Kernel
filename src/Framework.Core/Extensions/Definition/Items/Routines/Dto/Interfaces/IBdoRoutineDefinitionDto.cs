using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutineDefinitionDto : IBdoExtensionItemDefinitionDto
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