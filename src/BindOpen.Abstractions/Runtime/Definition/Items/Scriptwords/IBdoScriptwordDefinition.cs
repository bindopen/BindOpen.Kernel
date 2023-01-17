using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
using BindOpen.MetaData.Items;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordDefinition : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        DataValueTypes InputValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes OutputValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoScriptwordDomainedDelegate RuntimeScopedFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoScriptwordDelegate RuntimeBasicFunction { get; set; }

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
        IBdoMetaElementSpecSet ParameterSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoDictionary RepeatedParameterDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RepeatedParameterName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes RepeatedParameterValueType { get; set; }

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
        /// <param name="key"></param>
        /// <param name="defaultKey"></param>
        /// <returns></returns>
        string GetRepeatedParameterDescriptionText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star);

    }
}