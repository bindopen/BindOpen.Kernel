using System;
using System.Linq;
using System.Reflection;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoSpecExtensions
    {
        public static IBdoSpec UpdateFrom(
            this IBdoSpec spec,
            BdoPropertyAttribute att)
        {
            if (att != null)
            {
                if (!string.IsNullOrEmpty(att.Alias))
                {
                    spec ??= BdoData.NewSpec();
                    spec.Aliases = att.Alias?.Split(",").Select(q => q.Trim()).ToList();
                }

                if (att.MinDataItemNumber != null)
                {
                    spec ??= BdoData.NewSpec();
                    spec.MinDataItemNumber = att.MinDataItemNumber.Value;
                }

                if (att.MaxDataItemNumber != null)
                {
                    spec ??= BdoData.NewSpec();
                    spec.MaxDataItemNumber = att.MaxDataItemNumber.Value;
                }

                if (!string.IsNullOrEmpty(att.Description))
                {
                    spec ??= BdoData.NewSpec();
                    spec.Description = BdoData.NewDictionary(att.Description);
                }

                if (att.DataRequirement != RequirementLevels.Any)
                {
                    spec ??= BdoData.NewSpec();
                    spec.DataRequirement = att.DataRequirement;
                }

                if (!string.IsNullOrEmpty(att.DataRequirementExp))
                {
                    spec ??= BdoData.NewSpec();
                    spec.DataRequirementExp = att.DataRequirementExp;
                }

                if (att.Name != null)
                {
                    spec ??= BdoData.NewSpec();
                    spec.Name = att.Name;
                }

                if (att.GroupId != null)
                {
                    spec ??= BdoData.NewSpec();
                    spec.GroupId = att.GroupId;
                }

                if (att.Requirement != RequirementLevels.Any)
                {
                    spec ??= BdoData.NewSpec();
                    spec.Requirement = att.Requirement;
                }

                if (!string.IsNullOrEmpty(att.RequirementExp))
                {
                    spec ??= BdoData.NewSpec();
                    spec.RequirementExp = att.RequirementExp;
                }

                if (!string.IsNullOrEmpty(att.Title))
                {
                    spec ??= BdoData.NewSpec();
                    spec.Title = BdoData.NewDictionary(att.Title);
                }

                if (att.ValueType != DataValueTypes.Any)
                {
                    spec ??= BdoData.NewSpec();
                    spec.WithDataType(att.ValueType);
                }
            }

            return spec;
        }

        public static IBdoSpec UpdateFrom<TAtt>(
            this IBdoSpec spec,
            PropertyInfo info)
            where TAtt : Attribute
        {
            return UpdateFrom(spec, info, typeof(TAtt));
        }

        public static IBdoSpec UpdateFrom(
            this IBdoSpec spec,
            PropertyInfo info,
            Type attributeType)
        {
            if (info != null)
            {
                if (attributeType != null)
                {
                    foreach (var att in info.GetCustomAttributes(attributeType))
                    {
                        spec = spec.UpdateFrom((BdoPropertyAttribute)att);
                    }
                }

                if (spec != null)
                {
                    spec.Name = info.Name;

                    var type = info.PropertyType;
                    spec.WithDataType(spec.DataType.ValueType == DataValueTypes.Any ? type.GetValueType() : DataValueTypes.None);
                    spec.AsType(type);
                }
            }

            return spec;
        }

        public static IBdoSpec UpdateFrom(
            this IBdoSpec spec,
            ParameterInfo info,
            Type attributeType)
        {
            if (info != null)
            {
                if (attributeType != null)
                {
                    foreach (var att in info.GetCustomAttributes(attributeType))
                    {
                        spec = spec.UpdateFrom((BdoPropertyAttribute)att);
                    }

                    spec.IsStatic = info.GetCustomAttributes(typeof(BdoThisAttribute)).Any();
                }

                if (spec != null)
                {
                    spec.Name = info.Name;

                    var type = info.ParameterType;
                    spec.WithDataType(
                        spec.DataType.ValueType == DataValueTypes.Any ? type.GetValueType() : DataValueTypes.None,
                        type);
                }
            }

            return spec;
        }
    }
}
