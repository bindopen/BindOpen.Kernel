using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Items.Formats.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Entities.Definition.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityDefinitionDto : IAppExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        List<FormatDefinitionDto> FormatDefinitions { get; }
        
        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        EntityKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DataSchema> PossibleMetaSchemas { get; }

        /// <summary>
        /// 
        /// </summary>
        string ViewerClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSpecSet DetailSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IFormatDefinitionDto GetFormatWithId(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IFormatDefinitionDto GetFormatWithName(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <returns></returns>
        IFormatDefinitionDto GetFormatWithUniqueName(string uniqueName);
    }
}