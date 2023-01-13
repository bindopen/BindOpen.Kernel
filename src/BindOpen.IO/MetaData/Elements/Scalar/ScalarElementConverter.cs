using BindOpen.MetaData.Items;
using BindOpen.MetaData.References;
using System.Linq;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ScalarElementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ScalarElementDto ToDto(this IBdoMetaScalar poco)
        {
            if (poco == null) return null;

            ScalarElementDto dto = new()
            {
                Description = poco.Description?.ToDto(),
                Id = poco.Id,
                Index = poco.Index ?? -1,
                ItemReference = poco.ItemReference?.ToDto(),
                ItemScript = poco.ItemScript,
                Name = poco.Name,
                Detail = poco.Detail?.ToDto(),
                Title = poco.Title?.ToDto(),
                ValueType = poco.ValueType
            };
            poco.WithSpecifications(poco.Specs.Select(q => q?.ToDto()).Cast<IBdoMetaElementSpec>().ToArray());

            if (poco.ItemizationMode == DataItemizationMode.Valued)
            {
                var values = poco.GetItemList<object>().Select(q => q.ToString(poco.ValueType)).ToList();
                if (values.Count == 1)
                {
                    dto.Item = values.FirstOrDefault();
                }
                else
                {
                    dto.Items = values;
                }
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaScalar ToPoco(this ScalarElementDto dto)
        {
            if (dto == null) return null;

            BdoMetaScalar poco = new();

            poco
                .WithName(dto.Name)
                .WithItemReference(dto.ItemReference?.ToPoco())
                .WithItemScript(dto.ItemScript)
                .WithDetail(dto.Detail?.ToPoco())
                //.WithSpecifications(dto.Specifications.Select(q => q?.ToPoco()).ToArray());
                //.WithValueType(dto.Specification !=null ? dto.Specification.ValueType : dto.ValueType)
                .WithIndex(dto.Index < 0 ? null : dto.Index);

            if (!string.IsNullOrEmpty(dto.Item))
            {
                poco.WithItem(dto.Item);
            }
            else
            {
                var objects = dto.Items.Select(q => q.ToObject(poco.ValueType)).ToList();
                poco.WithItem(objects);
            }

            return poco;
        }
    }
}
