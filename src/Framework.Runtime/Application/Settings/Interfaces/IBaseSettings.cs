using System;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
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

        T Get<T>(string name);

        object Get(string name);

        T GetProperty<T>([CallerMemberName] string propertyName = null);

        T GetProperty<T>(T defaultValue, [CallerMemberName] string propertyName = null) where T : struct, IConvertible;

        void Set(string name, object value);

        void SetProperty(object value, [CallerMemberName] string propertyName = null);

        ILog UpdateFromFile(
            string filePath,
            SpecificationLevel[] specificationLevels = null,
            IDataElementSpecSet specificationSet = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            XmlSchemaSet xmlSchemaSet = null);
    }
}