using BindOpen.Scoping.Script;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a IO converter of meta data items.
    /// </summary>
    public static class MetaDataConverter
    {
        /// <summary>
        /// Converts a metadata poco into a DTO one.
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
            else if (poco is IBdoMetaNode set)
            {
                dto = set.ToDto();
            }

            return dto;
        }

        /// <summary>
        /// Converts a meta data DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
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
            else if (dto is MetaNodeDto list)
            {
                return list.ToPoco();
            }

            return poco;
        }
    }
}
