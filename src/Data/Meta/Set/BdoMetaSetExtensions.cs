using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoMetaSetExtensions
    {
        public static T WithNode<T>(this T set, string name, params object[] children)
            where T : class, IBdoMetaSet
        {
            var node = BdoData.NewNode(name).With(children);
            set.With(node);

            return set;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static T With<T>(
            this T set,
            params object[] items)
            where T : class, IBdoMetaSet
        {
            if (set != null && items != null)
            {
                foreach (var item in items)
                {
                    var meta = ConvertToMeta(item);
                    set.WithItems(meta);
                }
            }
            return set;
        }

        private static IBdoMetaData ConvertToMeta(object item)
        {
            // 1. Déjà un MetaData : on retourne tel quel
            if (item is IBdoMetaData existingMeta) return existingMeta;

            // 2. Gestion via ITuple (couvre (name, value) et (name, val1, val2...))
            if (item is ITuple tuple && tuple.Length >= 2 && tuple[0] is string name)
            {
                // --- Analyse du préfixe et du mode ---
                // On ne déclenche le mode spécial que s'il y a un caractère après le préfixe
                char prefix = (name.Length > 1 && (name[0] == '$' || name[0] == '#' || name[0] == '@'))
                              ? name[0] : '\0';

                // Gestion de l'échappement (ex: "$$name" -> nom réel "$name")
                if (name.Length > 1 && name[0] == name[1] && prefix != '\0')
                {
                    prefix = '\0';
                    name = name[1..];
                }

                string cleanName = (prefix != '\0') ? name[1..] : name;
                object value = tuple[1];

                // --- Création selon le mode identifié ---
                switch (prefix)
                {
                    case '$': // Mode NODE
                        var node = BdoData.NewNode(cleanName);
                        // Si le tuple a plus de 2 éléments, on traite le reste comme des enfants
                        if (tuple.Length > 2)
                        {
                            for (int i = 1; i < tuple.Length; i++)
                                node.With(ConvertToMeta(tuple[i]));
                        }
                        else if (IsTupleEnumerable(value))
                        {
                            node.With((value as IEnumerable<object>).Select(ConvertToMeta).ToArray());
                        }
                        else
                        {
                            node.With(value);
                        }
                        return node;

                    case '@': // Mode REFERENCE
                        return BdoData.NewReference(value?.ToString()).WithId(cleanName);

                    case '#': // Mode OBJECT
                        return BdoData.NewObject(cleanName).WithData(value);

                    default: // Mode SCALAR (ou nom spécial comme "#")
                             // Si la valeur est un tableau de tuples sans préfixe $, 
                             // on peut soit créer un Node auto, soit rester sur un Scalar Multi-Value.
                             // Ici, on suit ta logique : Enumerable de tuples = Node.
                        if (IsTupleEnumerable(value))
                        {
                            return BdoData.NewNode(cleanName).WithData(value as object[]);
                        }
                        return BdoData.NewScalar(cleanName, value);
                }
            }

            return null;
        }

        private static bool IsTupleEnumerable(object value)
        {
            if (value == null) return false;

            // Gestion des listes génériques et tableaux
            if (value is IEnumerable<object> enumerable)
            {
                var first = enumerable.FirstOrDefault();
                return first != null && IsSingleTuple(first);
            }

            var type = value.GetType();
            if (type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type))
            {
                var itemType = type.GetGenericArguments().FirstOrDefault();
                if (itemType != null)
                {
                    return itemType.Name.StartsWith("ValueTuple`2") || itemType.Name.StartsWith("Tuple`2");
                }
            }

            return false;
        }

        private static bool IsSingleTuple(object item)
        {
            // On utilise ITuple pour être universel (ValueTuple et Tuple)
            return item is ITuple tuple && tuple.Length >= 2 && tuple[0] is string;
        }

        // Add

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T InsertData<T>(
            this T list,
            object obj)
            where T : IBdoMetaSet
        {
            IBdoMetaData meta = obj is IBdoMetaData data ? data : BdoData.NewMeta(obj);
            list?.WithItems(meta);

            return list;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static T With<T>(
           this T set,
           string key,
           object value)
           where T : IBdoMetaSet
        {
            set.With((key, value));

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T With<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaSet
        {
            list.With(pairs.Select(q => (q.Key, q.Value)).ToArray());

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                foreach (var (Name, Value) in pairs)
                {
                    list.WithItems(BdoData.NewMeta(Name, Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                foreach (var (Name, ValueType, Value) in pairs)
                {
                    list.WithItems(BdoData.NewMeta(Name, ValueType, Value));
                }
            }

            return list;
        }

        // Tokens

        /// <summary>
        /// Gets the quoted string.
        /// </summary>
        /// <param key="st">The string to normalize.</param>
        /// <returns>Returns the quoted string.</returns>
        public static IBdoMetaSet ExtractTokenMetas(this string st, string pattern, char quote = '\'')
        {
            if (st == null || pattern == null) return null;

            var set = BdoData.NewSet();
            int tokenCount = 0;

            if (!string.IsNullOrEmpty(st) && pattern != null)
            {
                int i = 0;
                int ii = -1;
                int ji = 0;
                string tokenName = null;

                while (i > -1)
                {
                    i = pattern.IndexOfNextString("{{", i);

                    if (ii == -1)
                    {
                        ji = i;
                    }

                    if (ii > -1)
                    {
                        var word = pattern.ToSubstring(ii, i == -1 ? pattern.Length - 1 : i - 1);
                        var wordst = word.ToUnquoted(quote);
                        var jj = string.IsNullOrEmpty(wordst) ? st.Length : st.IndexOfNextString(wordst, ji, quote: quote);

                        if (jj > -1)
                        {
                            var tokenValue = st.ToSubstring(ji, jj - 1).ToUnquoted(quote);
                            set.With((tokenName, tokenValue));
                        }
                        ji = jj + word.Length;
                    }

                    if (i > -1)
                    {
                        int j = pattern.IndexOfNextString("}}", i + 2, quote: quote);

                        if (j > -1)
                        {
                            tokenCount++;
                            tokenName = pattern.ToSubstring(i + 2, j - 1);
                            ii = j + 2;
                        }
                        i++;
                    }
                }
            }

            return set?.Count == tokenCount ? set : null;
        }

        /// <summary>
        /// Gets the quoted string.
        /// </summary>
        /// <param key="st">The string to normalize.</param>
        /// <returns>Returns the quoted string.</returns>
        public static IEnumerable<string> ExtractTokens(this string pattern, char quote = '\'')
        {
            var set = new List<string>();

            if (!string.IsNullOrEmpty(pattern))
            {
                int i = 0;
                int ii = -1;
                string tokenName = null;

                while (i > -1)
                {
                    i = pattern.IndexOfNextString("{{", i);

                    if (ii > -1)
                    {
                        set.Add(tokenName);
                    }

                    if (i > -1)
                    {
                        int j = pattern.IndexOfNextString("}}", i + 2, quote: quote);

                        if (j > -1)
                        {
                            tokenName = pattern.ToSubstring(i + 2, j - 1);
                            ii = j + 2;
                        }
                        i = j + 1;
                    }
                }
            }

            return set;
        }

        /// <summary>
        /// Formats the specified string replacing the specified index by the specified string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <param key="index">The index to consider.</param>
        /// <param key="replaceString">The replacement string to consider.</param>
        /// <param key="wholeReplaceString">The whole replacement string to consider.</param>
        /// <example>The string should be formated this way: {0} {1} or { .. {0} .. } { .. {1} .. } and so on.</example>
        /// <returns>The formated string.</returns>
        public static string FormatFromTokens(this string pattern, IBdoMetaSet metaSet, char quote = '\'')
        {
            if (string.IsNullOrEmpty(pattern)) return pattern;

            var st = pattern;
            if (metaSet?.Any() == true)
            {
                int i = 0;
                string tokenName = null;

                while (i > -1)
                {
                    i = st.IndexOfNextString("{{", i);

                    if (i > -1)
                    {
                        int j = st.IndexOfNextString("}}", i + 2, quote: quote);

                        if (j > -1)
                        {
                            tokenName = st.ToSubstring(i + 2, j - 1);

                            var meta = metaSet[tokenName];
                            if (meta != null)
                            {
                                var newSt = meta.GetData<string>();
                                st = st[0..i] + newSt + st[(j + 2)..^0];
                                j -= (tokenName.Length - (newSt?.Length ?? 0) + 3);
                            }
                        }
                        i = j + 1;
                    }
                }
            }

            return st;
        }

    }
}