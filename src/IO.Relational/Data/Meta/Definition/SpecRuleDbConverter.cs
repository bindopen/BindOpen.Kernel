using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a rule converter.
    /// </summary>
    public static class SpecRuleDbConverter
    {
        /// <summary>
        /// Converts a requirement level conditional statement poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecRuleDb ToDb(this IBdoSpecRule poco)
        {
            if (poco == null) return null;

            var dbItem = new SpecRuleDb()
            {
                Condition = poco.Condition.ToDb(),
                GroupId = poco.GroupId,
                Identifier = poco.Identifier,
                Kind = poco.Kind,
                Reference = poco.Reference.ToDb(),
                ResultCode = poco.ResultCode,
                ResultDescription = poco.ResultDescription,
                ResultEventKind = poco.ResultEventKind,
                ResultTitle = poco.ResultTitle,
                Value = BdoData.NewScalar(poco.Value).ToDb()
            };

            return dbItem;
        }

        /// <summary>
        /// Converts a string conditional statement DTO into a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoSpecRule ToPoco(this SpecRuleDb dbItem)
        {
            if (dbItem == null) return null;

            var poco = new BdoSpecRule()
            {
                Condition = dbItem.Condition.ToPoco(),
                GroupId = dbItem.GroupId,
                Kind = dbItem.Kind,
                Identifier = dbItem.Identifier,
                Reference = dbItem.Reference.ToPoco(),
                ResultCode = dbItem.ResultCode,
                ResultDescription = dbItem.ResultDescription,
                ResultEventKind = dbItem.ResultEventKind,
                ResultTitle = dbItem.ResultTitle,
                Value = dbItem.Value
            };

            return poco;
        }
    }
}
