using BindOpen.System.Data.Meta;
using System.Linq;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class StringConditionalStatementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static StringConditionalStatementDto ToDto(this ITBdoConditionalStatement<string> poco)
        {
            if (poco == null) return null;

            StringConditionalStatementDto dto = new();
            dto.Items = new();
            dto.Items.AddRange(poco.Select(q => new StringConditionalStatementPairDto()
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
        public static ITBdoConditionalStatement<string> ToPoco(this StringConditionalStatementDto dto)
        {
            if (dto == null) return null;

            var poco = BdoData.NewStatement(dto.Items?.Select(q => (q.Item, q.Condition?.ToPoco()))?.ToArray());

            return poco;
        }
    }
}
