using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Extensions.Definition
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