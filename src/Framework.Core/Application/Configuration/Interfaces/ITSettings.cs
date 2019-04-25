using System;
using System.Runtime.CompilerServices;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Application.Configuration
{
    public interface ITSettings<T> : IDataItem, IReferenced where T : IConfiguration
    {
        IAppScope AppScope { get; }

        T Configuration { get; }


        Q Get<Q>(string name = null) where Q : class;
        Q GetProperty<Q>([CallerMemberName] string propertyName = null);

        Q GetProperty<Q>(Q defaultValue, [CallerMemberName] string propertyName = null) where Q : struct, IConvertible;
        void Set(object value, string name = null);
        void SetProperty(object value, [CallerMemberName] string propertyName = null);
    }
}