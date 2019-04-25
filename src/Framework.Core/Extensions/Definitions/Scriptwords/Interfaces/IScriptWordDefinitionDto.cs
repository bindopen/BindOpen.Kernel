using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Definitions.Scriptwords
{
    public interface IScriptwordDefinitionDto : IAppExtensionItemDefinitionDto
    {
        List<ScriptwordDefinitionDto> Children { get; set; }

        ScriptItemKind Kind { get; set; }
        bool IsRepeatedParameters { get; set; }
        int MaxParameterNumber { get; set; }
        int MinParameterNumber { get; set; }
        DataElementSpecSet ParameterSpecification { get; set; }
        string ReferenceUniqueName { get; set; }
        DictionaryDataItem RepeatedParameterDescription { get; set; }
        string RepeatedParameterName { get; set; }
        DataValueType RepeatedParameterValueType { get; set; }
        DataValueType ReturnValueType { get; set; }

        string CallingClass { get; set; }
        string RuntimeFunctionName { get; set; }

        string GetDefaultRuntimeFunctionName();
        string GetRepeatedParameterDescriptionText(string variantName = "*", string defaultVariantName = "*");
        string GetText(LogFormat logFormat = LogFormat.Xml, string uiCulture = "*");
    }
}