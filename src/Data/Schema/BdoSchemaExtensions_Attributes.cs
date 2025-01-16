using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using System;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data element set.
/// </summary>
public static partial class BdoSchemaExtensions
{
    public static bool UpdateFrom(
        this IBdoSchema schema,
        BdoPropertyAttribute att)
    {
        var change = false;

        if (schema != null && att != null)
        {
            if (att.Aliases?.Any() == true)
            {
                change = true;
                schema.Aliases = att.Aliases?.ToList();
            }

            if (!string.IsNullOrEmpty(att.Reference))
            {
                change = true;
                schema.Reference = BdoData.NewRef(att.Reference);
            }

            if (att.MinDataItemNumber != null)
            {
                change = true;
                schema.MinDataItemNumber = att.MinDataItemNumber.Value;
            }

            if (att.MaxDataItemNumber != null)
            {
                change = true;
                schema.MaxDataItemNumber = att.MaxDataItemNumber.Value;
            }

            if (!string.IsNullOrEmpty(att.Description))
            {
                change = true;
                schema.Description = BdoData.NewDictionary(att.Description);
            }

            if (att.ItemRequirement != RequirementLevels.Any)
            {
                change = true;

                string exp = null;

                if (!string.IsNullOrEmpty(att.ItemRequirementExp))
                {
                    exp = att.ItemRequirementExp;
                }
                schema.AddItemRequirement(att.ItemRequirement, (BdoExpressionCondition)exp);
            }

            if (att.Name != null)
            {
                schema.Name = att.Name;
            }

            if (att.GroupId != null)
            {
                change = true;
                schema.GroupId = att.GroupId;
            }

            if (att.Requirement != RequirementLevels.Any)
            {
                change = true;

                string exp = null;

                if (!string.IsNullOrEmpty(att.RequirementExp))
                {
                    exp = att.RequirementExp;
                }
                schema.AddRequirement(att.Requirement, (BdoExpressionCondition)exp);
            }

            if (!string.IsNullOrEmpty(att.Title))
            {
                change = true;
                schema.Title = BdoData.NewDictionary(att.Title);
            }

            if (att.ValueType != DataValueTypes.Any)
            {
                change = true;
                schema.WithDataType(att.ValueType);
            }
        }

        return change;
    }

    public static bool UpdateFrom<TAtt>(
        this IBdoSchema schema,
        PropertyInfo info)
        where TAtt : Attribute
    {
        return UpdateFrom(schema, info, typeof(TAtt));
    }

    public static bool UpdateFrom(
        this IBdoSchema schema,
        PropertyInfo info,
        Type attributeType)
    {
        var change = false;

        if (schema != null && info != null)
        {
            if (attributeType != null)
            {
                foreach (var att in info.GetCustomAttributes(attributeType))
                {
                    change |= schema.UpdateFrom((BdoPropertyAttribute)att);
                }
            }

            schema.Name ??= info.Name;

            var type = info.PropertyType;
            var valueType = schema.DataType.ValueType == DataValueTypes.Any ? type.GetValueType() : DataValueTypes.None;
            schema.WithDataType(valueType);
            if (valueType == DataValueTypes.Object)
            {
                schema.AsType(type);
            }
        }

        return change;
    }

    public static IBdoSchema UpdateFrom(
        this IBdoSchema schema,
        ParameterInfo info,
        Type attributeType)
    {
        var change = false;

        if (schema != null && info != null)
        {
            if (attributeType != null)
            {
                foreach (var att in info.GetCustomAttributes(attributeType))
                {
                    change |= schema.UpdateFrom((BdoPropertyAttribute)att);
                }

                schema.SetFlagValue(
                    BdoSchemaProperties.IsScriptParameter,
                    info.GetCustomAttributes(typeof(BdoScriptParameterAttribute)).Any());
            }

            schema.Name ??= info.Name;

            var type = info.ParameterType;
            schema.WithDataType(type);
        }

        return schema;
    }
}
