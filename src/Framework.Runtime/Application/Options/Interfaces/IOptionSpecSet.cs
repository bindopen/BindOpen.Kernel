using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Conditions;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Runtime.Application.Options
{
    public interface IOptionSpecSet : IDataItemSet<OptionSpec>, INamed, IIndexed
    {
        Condition Condition { get; set; }
        int Index { get; set; }
        string Name { get; set; }
        List<OptionSpecSet> SubSets { get; set; }

        IOptionSpecSet AddOption(DataValueType dataValueType, OptionNameKind nameKind, params string[] aliases);
        IOptionSpecSet AddOption(DataValueType dataValueType, RequirementLevel requirementLevel, OptionNameKind nameKind, params string[] aliases);
        IOptionSpecSet AddOption(OptionNameKind nameKind, params string[] aliases);
        IOptionSpecSet AddOption(params string[] aliases);
        IOptionSpecSet AddOption(RequirementLevel requirementLevel, OptionNameKind nameKind, params string[] aliases);
        IOptionSpecSet AddOption(RequirementLevel requirementLevel, params string[] aliases);
        IOptionSpecSet AddOption(Type type, RequirementLevel requirementLevel, OptionNameKind nameKind, params string[] aliases);
        IOptionSpecSet AddSubSet(ICondition condition, params IOptionSpec[] optionSpecifications);
        IOptionSpecSet AddSubSet(IOptionSpecSet subSet);
        IOptionSpecSet AddSubSet(params IOptionSpec[] optionSpecifications);
        void ClearOptions();
        string GetHelpText(string uiCulture = "*");
        bool HasOption(string name);
        IOptionSpecSet RemoveOption(string name);
    }
}