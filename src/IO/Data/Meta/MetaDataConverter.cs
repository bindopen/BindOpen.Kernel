using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaDataConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaDataDto ToDto(
            this IBdoMetaData poco)
        {
            if (poco == null) return null;

            MetaDataDto dto = null;

            if (poco is IBdoScriptword script)
            {
                dto = ScriptwordConverter.ToDto(script);
            }
            else if (poco is IBdoMetaObject obj)
            {
                dto = obj.ToDto();
            }
            else if (poco is IBdoMetaScalar scalar)
            {
                dto = scalar.ToDto();
            }
            else if (poco is IBdoMetaComposite set)
            {
                dto = set.ToDto();
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaData ToPoco(
            this MetaDataDto dto)
        {
            if (dto == null) return null;

            BdoMetaData poco = null;

            if (dto is ScriptwordDto script)
            {
                return ScriptwordConverter.ToPoco(script);
            }
            else if (dto is MetaObjectDto obj)
            {
                return obj.ToPoco();
            }
            else if (dto is MetaScalarDto scalar)
            {
                return scalar.ToPoco();
            }
            else if (dto is MetaCompositeDto list)
            {
                return list.ToPoco();
            }

            return poco;
        }
    }
}
