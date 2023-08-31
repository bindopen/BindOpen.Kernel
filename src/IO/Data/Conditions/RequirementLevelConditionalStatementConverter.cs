using BindOpen.System.Data.Meta;
using System.Linq;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class RequirementLevelConditionalStatementConverter
    {
        /// <summary>
        /// Converts to DTO.
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
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ITBdoConditionalStatement<RequirementLevels> ToPoco(this RequirementLevelConditionalStatementDto dto)
        {
            if (dto == null) return null;

            var poco = BdoData.NewStatement(dto.Items?.Select(q => (q.Item, q.Condition?.ToPoco()))?.ToArray());

            return poco;
        }
    }
}
