﻿using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Common;

namespace BindOpen.Framework.Core.Extensions.Definitions.Libraries
{
    public interface ILibraryDefinitionDto : IDescribedDataItem
    {
        string AssemblyName { get; set; }
        string FileName { get; set; }
        string GroupName { get; set; }
        DictionaryDataItem ItemIndexFullNameDictionary { get; set; }
        string RootNamespace { get; set; }
        List<string> UsingAssemblyFileNames { get; set; }

        string GetRootNamespace();

        void Initialize();
    }
}