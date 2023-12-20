using BindOpen.Data.Conditions;
using System;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoSpecExtensions
    {
        public static bool UpdateFrom(
            this IBdoSpec spec,
            BdoPropertyAttribute att)
        {
            var change = false;

            if (spec != null && att != null)
            {
                if (att.Aliases?.Any() == true)
                {
                    change = true;
                    spec.Aliases = att.Aliases?.ToList();
                }

                if (!string.IsNullOrEmpty(att.Reference))
                {
                    change = true;
                    spec.Reference = BdoData.NewRef(att.Reference);
                }

                if (att.MinDataItemNumber != null)
                {
                    change = true;
                    spec.MinDataItemNumber = att.MinDataItemNumber.Value;
                }

                if (att.MaxDataItemNumber != null)
                {
                    change = true;
                    spec.MaxDataItemNumber = att.MaxDataItemNumber.Value;
                }

                if (!string.IsNullOrEmpty(att.Description))
                {
                    change = true;
                    spec.Description = BdoData.NewDictionary(att.Description);
                }

                if (att.ItemRequirement != RequirementLevels.Any)
                {
                    change = true;

                    string exp = null;

                    if (!string.IsNullOrEmpty(att.ItemRequirementExp))
                    {
                        exp = att.ItemRequirementExp;
                    }
                    spec.AddItemRequirement(att.ItemRequirement, (BdoExpressionCondition)exp);
                }

                if (att.Name != null)
                {
                    spec.Name = att.Name;
                }

                if (att.GroupId != null)
                {
                    change = true;
                    spec.GroupId = att.GroupId;
                }

                if (att.Requirement != RequirementLevels.Any)
                {
                    change = true;

                    string exp = null;

                    if (!string.IsNullOrEmpty(att.RequirementExp))
                    {
                        exp = att.RequirementExp;
                    }
                    spec.AddRequirement(att.Requirement, (BdoExpressionCondition)exp);
                }

                if (!string.IsNullOrEmpty(att.Title))
                {
                    change = true;
                    spec.Title = BdoData.NewDictionary(att.Title);
                }

                if (att.ValueType != DataValueTypes.Any)
                {
                    change = true;
                    spec.WithDataType(att.ValueType);
                }
            }

            return change;
        }

        public static bool UpdateFrom<TAtt>(
            this IBdoSpec spec,
            PropertyInfo info)
            where TAtt : Attribute
        {
            return UpdateFrom(spec, info, typeof(TAtt));
        }

        public static bool UpdateFrom(
            this IBdoSpec spec,
            PropertyInfo info,
            Type attributeType)
        {
            var change = false;

            if (spec != null && info != null)
            {
                if (attributeType != null)
                {
                    foreach (var att in info.GetCustomAttributes(attributeType))
                    {
                        change |= spec.UpdateFrom((BdoPropertyAttribute)att);
                    }
                }

                spec.Name ??= info.Name;

                var type = info.PropertyType;
                spec.WithDataType(spec.DataType.ValueType == DataValueTypes.Any ? type.GetValueType() : DataValueTypes.None);
                spec.AsType(type);
            }

            return change;
        }

        public static IBdoSpec UpdateFrom(
            this IBdoSpec spec,
            ParameterInfo info,
            Type attributeType)
        {
            var change = false;

            if (spec != null && info != null)
            {
                if (attributeType != null)
                {
                    foreach (var att in info.GetCustomAttributes(attributeType))
                    {
                        change |= spec.UpdateFrom((BdoPropertyAttribute)att);
                    }

                    spec.AsFlag(
                        BdoSpecProperties.IsScriptParameter,
                        info.GetCustomAttributes(typeof(BdoScriptParameterAttribute)).Any());
                }

                spec.Name ??= info.Name;

                var type = info.ParameterType;
                spec.WithDataType(type);
            }

            return spec;
        }
    }
}
