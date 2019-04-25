﻿using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Definitions;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    public interface ITAppExtensionItemIndex<T> : IDataItem, IIdentified where T : IAppExtensionItemDefinition
    {
        List<T> Definitions { get; }

        void SetDefinitions(List<T> definitions);
    }
}