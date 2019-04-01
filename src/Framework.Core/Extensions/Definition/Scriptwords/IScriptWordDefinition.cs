using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Runtime.Scriptwords;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Definition.Scriptwords
{
    public interface IScriptWordDefinition : IAppExtensionItemDefinition
    {
        string CallingClass { get; set; }
        List<IScriptWordDefinition> Children { get; set; }
        bool IsRepeatedParameters { get; set; }
        ScriptItemKind Kind { get; set; }
        int MaxParameterNumber { get; set; }
        int MinParameterNumber { get; set; }
        IDataElementSpecSet ParameterSpecification { get; set; }
        string ReferenceUniqueName { get; set; }
        IDictionaryDataItem RepeatedParameterDescription { get; set; }
        string RepeatedParameterName { get; set; }
        DataValueType RepeatedParameterValueType { get; set; }
        DataValueType ReturnValueType { get; set; }
        string RuntimeFunctionName { get; set; }

        string GetDefaultRuntimeFunctionName();
        string GetRepeatedParameterDescriptionText(string variantName = "*", string defaultVariantName = "*");
        string GetText(LogFormat logFormat = LogFormat.Xml, string uiCulture = "*");
        ScriptWordFunction RuntimeFunction { get; set; }
    }
}