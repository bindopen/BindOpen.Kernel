using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;

namespace BindOpen.Framework.Core.Extensions.Definition.Handlers
{
    public interface IHandlerDefinition : IAppExtensionItemDefinition
    {
        string CallingClass { get; set; }
        string GetFunctionName { get; set; }
        IDataElementSpecSet ParameterSpecification { get; set; }
        string PostFunctionName { get; set; }
        IDataElementSpec SourceSpecification { get; set; }
        IDataElementSpec TargetSpecification { get; set; }
    }
}