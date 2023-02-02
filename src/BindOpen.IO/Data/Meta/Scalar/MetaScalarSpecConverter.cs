﻿using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaScalarSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaScalarSpecDto ToDto(this IBdoMetaScalarSpec poco)
        {
            if (poco == null) return null;

            MetaScalarSpecDto dto = new()
            {
                AccessibilityLevel = poco.AccessibilityLevel,
                Aliases = poco.Aliases?.ToList(),
                AvailableItemizationModes = poco.ItemizationModes?.ToList(),
                ConstraintStatement = poco.ConstraintStatement?.ToDto(),
                InheritanceLevel = poco.InheritanceLevel,
                IsAllocatable = poco.IsAllocatable,
                ItemExpression = poco.ItemExpression?.ToDto(),
                ItemSpecificationLevels = poco.ItemSpecificationLevels?.ToList(),
                MaximumItemNumber = poco.MaximumItemNumber,
                MinimumItemNumber = poco.MinimumItemNumber,
                RequirementLevel = poco.RequirementLevel,
                RequirementExpression = poco.RequirementExpression?.ToDto(),
                SpecificationLevels = poco.SpecificationLevels?.ToList(),
                ValueType = poco.DataValueType
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaScalarSpec ToPoco(this MetaScalarSpecDto dto)
        {
            if (dto == null) return null;

            BdoMetaScalarSpec poco = new()
            {
                //AccessibilityLevel = dto.AccessibilityLevel,
                //Aliases = dto.Aliases?.ToList(),
                //AreaSpecifications = dto.AreaSpecifications.Select(q => q.ToPoco()).ToList(),
                //AvailableItemizationModes = dto.AvailableItemizationModes?.ToList(),
                //ConstraintStatement = dto.ConstraintStatement?.ToPoco(),
                //GroupId = dto.GroupId,
                //InheritanceLevel = dto.InheritanceLevel,
                //IsAllocatable = dto.IsAllocatable,
                //ItemScript = dto.ItemScript,
                //ItemSpecificationLevels = dto.ItemSpecificationLevels?.ToList(),
                //MaximumItemNumber = dto.MaximumItemNumber,
                //MinimumItemNumber = dto.MinimumItemNumber,
                //RequirementLevel = dto.RequirementLevel,
                //RequirementScript = dto.RequirementScript,
                //SpecificationLevels = dto.SpecificationLevels?.ToList(),
                //ValueType = dto.ValueType
            };

            return poco;
        }
    }
}
