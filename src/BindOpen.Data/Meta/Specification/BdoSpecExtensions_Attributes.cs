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
        public static void UpdateFrom(
            this IBdoSpec spec,
            BdoPropertyAttribute att)
        {
            if (spec != null && att != null)
            {
                if (!string.IsNullOrEmpty(att.Alias))
                {
                    spec.Aliases = att.Alias?.Split(",").Select(q => q.Trim()).ToList();
                }

                if (att.MinDataItemNumber != null)
                {
                    spec.MinDataItemNumber = att.MinDataItemNumber.Value;
                }

                if (att.MaxDataItemNumber != null)
                {
                    spec.MaxDataItemNumber = att.MaxDataItemNumber.Value;
                }

                if (!string.IsNullOrEmpty(att.Description))
                {
                    spec.Description = BdoData.NewDictionary(att.Description);
                }

                if (att.DataRequirement != RequirementLevels.Any)
                {
                    spec.DataRequirement = att.DataRequirement;
                }

                if (!string.IsNullOrEmpty(att.DataRequirementExp))
                {
                    spec.DataRequirementExp = att.DataRequirementExp;
                }

                if (att.Name != null)
                {
                    spec.Name = att.Name;
                }

                if (att.GroupId != null)
                {
                    spec.GroupId = att.GroupId;
                }

                if (att.Requirement != RequirementLevels.Any)
                {
                    spec.Requirement = att.Requirement;
                }

                if (!string.IsNullOrEmpty(att.RequirementExp))
                {
                    spec.RequirementExp = att.RequirementExp;
                }

                if (!string.IsNullOrEmpty(att.Title))
                {
                    spec.Title = BdoData.NewDictionary(att.Title);
                }

                if (att.ValueType != DataValueTypes.Any)
                {
                    spec.ValueType = att.ValueType;
                }
            }
        }

        public static void UpdateFrom<TAtt>(
            this IBdoSpec spec,
            PropertyInfo info)
            where TAtt : Attribute
        {
            UpdateFrom(spec, info, typeof(TAtt));
        }

        public static void UpdateFrom(
            this IBdoSpec spec,
            PropertyInfo info,
            Type attributeType)
        {
            if (spec != null && info != null)
            {
                spec.Name = info.Name;

                if (attributeType != null)
                {
                    foreach (var att in info.GetCustomAttributes(attributeType))
                    {
                        spec.UpdateFrom((BdoPropertyAttribute)att);
                    }
                }

                var type = info.PropertyType;
                spec.ValueType = spec.ValueType == DataValueTypes.Any ? type.GetValueType() : DataValueTypes.None;
                spec.AsType(type);
            }
        }

        public static void UpdateFrom(
            this IBdoSpec spec,
            ParameterInfo info,
            Type attributeType)
        {
            if (spec != null && info != null)
            {
                spec.Name = info.Name;

                if (attributeType != null)
                {
                    foreach (var att in info.GetCustomAttributes(attributeType))
                    {
                        spec.UpdateFrom((BdoPropertyAttribute)att);
                    }
                }

                var type = info.ParameterType;
                spec.ValueType = spec.ValueType == DataValueTypes.Any ? type.GetValueType() : DataValueTypes.None;
                spec.AsType(type);
            }
        }
    }
}
