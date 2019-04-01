using System;
using System.Runtime.CompilerServices;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;

namespace BindOpen.Framework.Core.Application.Configuration
{
    public interface IConfiguration : IDataElementSet
    {
        IAppScope AppScope { get; set; }
        string CreationDate { get; set; }
        string CurrentFilePath { get; set; }
        string LastModificationDate { get; set; }

        T Get<T>([CallerMemberName] string propertyName = null);
        T Get<T>(T defaultValue, [CallerMemberName] string propertyName = null) where T : struct, IConvertible;
        void Set(object value, [CallerMemberName] string propertyName = null);
    }
}