﻿using BindOpen.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntity : ITBdoExtensionItem<IBdoEntityDefinition, IBdoEntityConfiguration, IBdoEntity>, INamed
    {
    }
}