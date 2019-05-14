using System;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    public interface IBaseSettings : IDataItem, IReferenced
    {
        IAppScope AppScope { get; }

        IBaseConfiguration Configuration { get; }

        Q Get<Q>(string name = null) where Q : class;
        Q GetProperty<Q>([CallerMemberName] string propertyName = null);

        Q GetProperty<Q>(Q defaultValue, [CallerMemberName] string propertyName = null) where Q : struct, IConvertible;
        void Set(object value, string name = null);
        void SetProperty(object value, [CallerMemberName] string propertyName = null);
        ILog Load(
            string filePath,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            XmlSchemaSet xmlSchemaSet = null);
    }
}