﻿using BindOpen.Data.Meta;
using BindOpen.Extensions.Scripting;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithDataExpression<T>(
            this T meta,
            string text,
            BdoExpressionKind kind = BdoExpressionKind.Auto)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.WithDataMode(DataMode.Expression);
                meta.DataExpression = BdoData.NewExp(text, kind);
            }

            return meta;
        }


        /// <summary>
        /// 
        /// </summary>
        public static T WithDataExpression<T>(
            this T meta,
            IBdoScriptword word)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.WithDataMode(DataMode.Expression);
                meta.DataExpression = BdoData.NewExp(word);
            }

            return meta;
        }
    }
}
