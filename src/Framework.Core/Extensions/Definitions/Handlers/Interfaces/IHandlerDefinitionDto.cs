using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;

namespace BindOpen.Framework.Core.Extensions.Definitions.Handlers
{
    public interface IHandlerDefinitionDto : IAppExtensionItemDefinitionDto
    {
        string CallingClass { get; set; }
        string GetFunctionName { get; set; }
        DataElementSpecSet ParameterSpecification { get; set; }
        string PostFunctionName { get; set; }
        DataElementSpec SourceSpecification { get; set; }
        DataElementSpec TargetSpecification { get; set; }
    }
}