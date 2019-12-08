using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents a globally titled DTO.
    /// </summary>
    public interface IGloballyTitled
    {
        /// <summary>
        /// The global title of this instance.
        /// </summary>
        DictionaryDataItem Title
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        void AddTitle(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        void AddTitle(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variantName"></param>
        /// <param name="defaultVariantName"></param>
        /// <returns></returns>
        string GetTitle(string variantName = "*", string defaultVariantName = "*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        void SetTitle(string key = "*", string text = "*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        void SetTitle(string text);
    }
}
