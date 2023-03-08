using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping.Scopes;

namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoScopeExtensions
    {

        public static object CallFunction(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var objs = config.GetDataList(scope, varSet, log)?.ToArray();
            return scope.CallFunction(config.DefinitionUniqueName, objs, varSet, log);
        }

        public static object CallFunction(
            this IBdoScope scope,
            string functionUniqueName,
            object[] objs,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (scope?.Check(true, log: log) == true)
            {
                var def = scope?.ExtensionStore?.GetDefinition(
                BdoExtensionKind.Function,
                functionUniqueName);

                if (def is not IBdoFunctionDefinition funcDef)
                {
                    log?.AddError(string.Format("Function ('{0}') not found", functionUniqueName));
                    return null;
                }

                var result = funcDef.RuntimeFunction.DynamicInvoke(objs);

                return result;
            }

            return null;
        }

        public static T CallFunction<T>(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = scope?.CallFunction(config, varSet, log);
            return obj.As<T>();
        }

        public static T CallFunction<T>(
            this IBdoScope scope,
            string functionUniqueName,
            object[] objs,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = scope?.CallFunction(functionUniqueName, objs, varSet, log);
            return obj.As<T>();
        }

    }
}
