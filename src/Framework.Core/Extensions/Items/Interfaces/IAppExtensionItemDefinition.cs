﻿using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Libraries;

namespace BindOpen.Framework.Core.Extensions.Items
{
    public interface IAppExtensionItemDefinition : IDataItem, IReferenced
    {
        ILibrary Library { get; }
    }
}