using BindOpen.Kernel.Data.Meta;
using System.Linq;

namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// This class represents a IO converter of string conditional statements.
    /// </summary>
    public static class StringConditionalStatementConverter
    {
        /// <summary>
        /// Converts string conditional statement poco to a DTO one.
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
        /// Converts string conditional statement DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static ITBdoConditionalStatement<string> ToPoco(this StringConditionalStatementDto dto)
        {
            if (dto == null) return null;

            var poco = BdoData.NewStatement(dto.Items?.Select(q => (q.Item, q.Condition?.ToPoco()))?.ToArray());

            return poco;
        }
    }
}
