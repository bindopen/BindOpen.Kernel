using BindOpen.Data.Meta;
using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ScalarSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ScalarSpecDto ToDto(this IBdoScalarSpec poco)
        {
            if (poco == null) return null;

            ScalarSpecDto dto = new()
            {
                AccessibilityLevel = poco.AccessibilityLevel,
                Aliases = poco.Aliases?.ToList(),
                AvailableValueModes = poco.ValueModes?.ToList(),
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
                ValueType = poco.ValueType
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoScalarSpec ToPoco(
            this ScalarSpecDto dto)
        {
            if (dto == null) return null;

            BdoScalarSpec poco = new()
            {
                //AccessibilityLevel = dto.AccessibilityLevel,
                //Aliases = dto.Aliases?.ToList(),
                //AreaSpecifications = dto.AreaSpecifications.Select(q => q.ToPoco()).ToList(),
                //AvailableValueModes = dto.AvailableValueModes?.ToList(),
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
