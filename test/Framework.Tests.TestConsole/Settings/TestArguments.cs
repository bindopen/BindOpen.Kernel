using BindOpen.Framework.Core.Application.Arguments;
using BindOpen.Framework.Core.Application.Options;
using BindOpen.Framework.Core.Data.Business.Conditions;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.TestConsole.Settings
{
    /// <summary>
    /// This class represents a test of arguments.
    /// </summary>
    public static class TestArguments
    {
        public static OptionSpecSet GetOptionSpecSet() =>
            new OptionSpecSet(
                new OptionSpec(OptionNameKind.OnlyValue) { Name= "operation", Index = 1 }
            )
            .AddSubSet(
                new OptionSpecSet(
                    new BasicCondition("operation", ConditionOperator.EqualTo, "build"),
                    new OptionSpec(OptionNameKind.OnlyValue) { Name = "object", Index = 1 }
                )
                .AddSubSet(
                    new OptionSpecSet(
                        new BasicCondition("object", ConditionOperator.EqualTo, "activities"),
                        new OptionSpec(OptionNameKind.OnlyValue, "{{*}}") { Name = "names", Index = 1, Title = "dd" },
                        new OptionSpec(OptionNameKind.OnlyValue) { Name = "outputFolder", Index = 2 },
                        new OptionSpec(OptionNameKind.NameThenValue, "--exclude", "-exc"),
                        new OptionSpec(OptionNameKind.NameThenValue, "--include", "-inc"),
                        new OptionSpec("--design", "-dsgn"),
                        new OptionSpec(OptionNameKind.OnlyName, "--config", "-cfg")
                    )
                    { Index = 2 }
                )
            );

        public static Log Test()
        {
            Log log = new Log();

            string[] arguments = { "export", "activities", "*" ,".", "--exclude", "\"dd\"", "--design", "--config" };

            OptionSet optionSet = arguments.UpdateOptions(GetOptionSpecSet(), false, log);

            return log;
        }
    }
}
