using System.Xml.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class TBdoDictionaryExtensions
    {
        public static TMetaSet ToMetaset<TMetaSet, TItem>(
            this ITBdoDictionary<TItem> dictionary)
            where TMetaSet : class, IBdoMetaSet, new()
        {
            if (dictionary == null) return default;

            var set = BdoData.NewSet<TMetaSet>();

            foreach (var item in dictionary)
            {
                set.Add(item.Key, item.Value);
            }

            return set;
        }

        public static IBdoMetaSet ToMetaset<TItem>(
            this ITBdoDictionary<TItem> dictionary)
            => ToMetaset<BdoMetaSet, TItem>(dictionary);

        public static string ToHtml<TItem>(
            this ITBdoDictionary<TItem> dictionary,
            string className = null)
        {
            if (dictionary == null) return "";

            var el = new XElement("table");

            if (!string.IsNullOrEmpty(className))
            {
                el.Add(new XAttribute("class", className));
            }

            foreach (var item in dictionary)
            {
                el.Add(
                    new XElement(
                        "tr",
                        new XElement("td", item.Key),
                        new XElement("td", item.Value)));
            }

            var html = el.ToString();
            return html;
        }
    }
}
