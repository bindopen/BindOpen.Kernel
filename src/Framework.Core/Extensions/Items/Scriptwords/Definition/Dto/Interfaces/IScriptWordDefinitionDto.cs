using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptwordDefinitionDto : IAppExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        List<ScriptwordDefinitionDto> Children { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsRepeatedParameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int MaxParameterNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int MinParameterNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSpecSet ParameterSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ReferenceUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DictionaryDataItem RepeatedParameterDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RepeatedParameterName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueType RepeatedParameterValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueType ReturnValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string CallingClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RuntimeFunctionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetDefaultRuntimeFunctionName();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variantName"></param>
        /// <param name="defaultVariantName"></param>
        /// <returns></returns>
        string GetRepeatedParameterDescriptionText(string variantName = "*", string defaultVariantName = "*");
    }
}