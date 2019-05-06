﻿using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Entities
{
    public interface IEntity : ITAppExtensionItem<EntityDefinition>, INamed
    {
        IObjectElement AsElement(string name = null, ILog log = null);
    }
}