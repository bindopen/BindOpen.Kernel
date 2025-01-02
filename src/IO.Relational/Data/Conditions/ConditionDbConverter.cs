using AutoMapper;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a Db converter of basic conditions.
    /// </summary>
    public static class ConditionDbConverter
    {
        /// <summary>
        /// Converts a basic condition poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConditionDb ToDb(this IBdoCondition poco)
        {
            ConditionDb dbItem = new();
            dbItem.UpdateFromPoco(poco);

            return dbItem;
        }

        public static ConditionDb UpdateFromPoco(
            this ConditionDb dbItem,
            IBdoCondition poco)
        {
            if (dbItem == null) return null;

            if (poco == null) return dbItem;

            MapperConfiguration config;
            Mapper mapper;

            switch (dbItem.Kind)
            {
                case BdoConditionKind.Basic:
                    if (poco is IBdoBasicCondition basicCondition)
                    {
                        config = new MapperConfiguration(
                            cfg => cfg.CreateMap<BdoBasicCondition, ConditionDb>()
                        );

                        mapper = new Mapper(config);
                        mapper.Map(basicCondition, dbItem);
                    }
                    break;
                case BdoConditionKind.Composite:
                    if (poco is IBdoCompositeCondition compositeCondition)
                    {
                        config = new MapperConfiguration(
                            cfg => cfg.CreateMap<BdoCompositeCondition, ConditionDb>()
                                .ForMember(q => q.Conditions, opt => opt.Ignore())
                        );

                        mapper = new Mapper(config);
                        mapper.Map(compositeCondition, dbItem);

                        dbItem.CompositionKind = compositeCondition.CompositionKind;
                        dbItem.Conditions = compositeCondition.Conditions?.Select(q => q.ToDb()).ToList();
                        dbItem.ParentId = compositeCondition.Parent?.Identifier;
                    }
                    break;
                case BdoConditionKind.Expression:
                    if (poco is IBdoExpression expressionCondition)
                    {
                        config = new MapperConfiguration(
                            cfg => cfg.CreateMap<BdoExpressionCondition, ConditionDb>()
                        );

                        mapper = new Mapper(config);
                        mapper.Map(expressionCondition, dbItem);
                    }
                    break;
            }

            return dbItem;
        }

        /// <summary>
        /// Converts a basic condition DTO into a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoCondition ToPoco(
            this ConditionDb dbItem)
        {
            if (dbItem == null) return null;

            switch (dbItem.Kind)
            {
                case BdoConditionKind.Basic:
                    return new BdoBasicCondition()
                    {
                        Argument1 = dbItem.ArgumentMetaData1?.ToPoco(),
                        Argument2 = dbItem.ArgumentMetaData2?.ToPoco(),
                        Identifier = dbItem.Identifier,
                        Operator = dbItem.Operator,
                        Name = dbItem.Name,
                        Parent = null
                    };
                case BdoConditionKind.Composite:
                    return new BdoCompositeCondition()
                    {
                        CompositionKind = dbItem.CompositionKind,
                        Conditions = dbItem.Conditions?.Select(q => q.ToPoco()).ToList(),
                        Identifier = dbItem.Identifier,
                        Name = dbItem.Name,
                        Parent = null
                    };
                case BdoConditionKind.Expression:
                    return new BdoExpressionCondition()
                    {
                        Expression = dbItem.ExpressionItem.ToPoco(),
                        Identifier = dbItem.Identifier,
                        Name = dbItem.Name,
                        Parent = null
                    };
            }

            return null;
        }
    }
}
