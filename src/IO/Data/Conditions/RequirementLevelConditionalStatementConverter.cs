using BindOpen.Kernel.Data.Meta;
using System.Linq;

namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// This class represents a IO converter of requirement level conditional statements.
    /// </summary>
    public static class RequirementLevelConditionalStatementConverter
    {
        /// <summary>
        /// Converts a requirement level conditional statement poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static RequirementLevelConditionalStatementDto ToDto(this ITBdoConditionalStatement<RequirementLevels> poco)
        {
            if (poco == null) return null;

            RequirementLevelConditionalStatementDto dto = new();
            dto.Items = new();
            dto.Items.AddRange(poco.Select(q => new RequirementLevelConditionalStatementPairDto()
            {
                Item = q.Item,
                Condition = q.Condition?.ToDto()
            }));

            return dto;
        }

        /// <summary>
        /// Converts a string conditional statement DTO into a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static ITBdoConditionalStatement<RequirementLevels> ToPoco(this RequirementLevelConditionalStatementDto dto)
        {
            if (dto == null) return null;

            var poco = BdoData.NewStatement(dto.Items?.Select(q => (q.Item, q.Condition?.ToPoco()))?.ToArray());

            return poco;
        }
    }
}
