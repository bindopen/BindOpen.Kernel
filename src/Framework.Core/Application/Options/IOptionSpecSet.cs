using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Business.Conditions;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Dto;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Core.Application.Options
{
    public interface IOptionSpecSet : IDataItemSet<IOptionSpec>, INamed, IIndexed
    {
        Condition Condition { get; set; }
        int Index { get; set; }
        string Name { get; set; }
        List<OptionSpecSet> SubSets { get; set; }

        OptionSpecSet AddOption(DataValueType dataValueType, OptionNameKind nameKind, params string[] aliases);
        OptionSpecSet AddOption(DataValueType dataValueType, RequirementLevel requirementLevel, OptionNameKind nameKind, params string[] aliases);
        OptionSpecSet AddOption(OptionNameKind nameKind, params string[] aliases);
        OptionSpecSet AddOption(params string[] aliases);
        OptionSpecSet AddOption(RequirementLevel requirementLevel, OptionNameKind nameKind, params string[] aliases);
        OptionSpecSet AddOption(RequirementLevel requirementLevel, params string[] aliases);
        OptionSpecSet AddOption(Type type, RequirementLevel requirementLevel, OptionNameKind nameKind, params string[] aliases);
        OptionSpecSet AddSubSet(ICondition condition, params IOptionSpec[] optionSpecifications);
        OptionSpecSet AddSubSet(IOptionSpecSet subSet);
        OptionSpecSet AddSubSet(params IOptionSpec[] optionSpecifications);
        void ClearOptions();
        string GetHelpText(string uiCulture = "*");
        OptionSpec GetItem(string key);
        bool HasOption(string name);
        OptionSpecSet RemoveOption(string name);
    }
}