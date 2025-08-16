using BindOpen.Scoping.Script;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a IO converter of meta data items.
    /// </summary>
    public static class MetaDataConverter
    {
        /// <summary>
        /// Converts an assembly reference poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaDataDto ToDto(this IBdoMetaData poco)
        {
            if (poco == null) return null;

            if (poco is IBdoScriptword script)
            {
                var dto = ScriptwordIOConverter.ToDto(script);
                return dto;
            }
            else if (poco is IBdoMetaObject obj)
            {
                var dto = obj.ToDto();
                return dto;
            }
            else if (poco is IBdoMetaScalar scalar)
            {
                var dto = scalar.ToDto();
                return dto;
            }
            else if (poco is IBdoMetaNode set)
            {
                var dto = set.ToDto();
                return dto;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="poco"></param>
        /// <returns></returns>
        public static MetaDataDto UpdateFromPoco(
            this MetaDataDto dto,
            IBdoMetaData poco)
        {
            if (poco == null) return null;

            if (dto is ScriptwordDto scriptDto && poco is IBdoScriptword script)
            {
                scriptDto.UpdateFromPoco(script);
            }
            else if (dto is MetaObjectDto metaObjectDto && poco is IBdoMetaObject obj)
            {
                metaObjectDto.UpdateFromPoco(obj);
            }
            else if (dto is MetaScalarDto scalarDto && poco is IBdoMetaScalar scalar)
            {
                scalarDto.UpdateFromPoco(scalar);
            }
            else if (dto is MetaNodeDto metaNodeDto && poco is IBdoMetaNode set)
            {
                metaNodeDto.UpdateFromPoco(set);
            }

            return dto;
        }

        /// <summary>
        /// Converts an assembly reference DTO into a poco one.
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
                return ScriptwordIOConverter.ToPoco(script);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poco"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static IBdoMetaData UpdateFromDto(
            this IBdoMetaData poco,
            MetaDataDto dto)
        {
            if (dto == null) return null;

            if (dto is ScriptwordDto script)
            {
                return poco.UpdateFromDto(script);
            }
            else if (dto is MetaObjectDto obj)
            {
                return poco.UpdateFromDto(obj);
            }
            else if (dto is MetaScalarDto scalar)
            {
                return poco.UpdateFromDto(scalar);
            }
            else if (dto is MetaNodeDto list)
            {
                return poco.UpdateFromDto(list);
            }

            return poco;
        }
    }
}
