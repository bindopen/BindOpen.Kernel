using BindOpen.System.Data.Meta;
using System.Linq;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class TConditionalStatementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static TConditionalStatementDto<TItem> ToDto<TItem>(this ITBdoConditionalStatement<TItem> poco)
        {
            if (poco == null) return null;

            TConditionalStatementDto<TItem> dto = new();
            dto.Items = new();
            dto.Items.AddRange(poco.Select(q => q.ToDto()));

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ITBdoConditionalStatement<TItem> ToPoco<TItem>(this TConditionalStatementDto<TItem> dto)
        {
            if (dto == null) return null;

            var poco = BdoData.NewStatement<TItem>(dto.Items?.Select(q => q.ToPoco())?.ToArray());

            return poco;
        }
    }
}
