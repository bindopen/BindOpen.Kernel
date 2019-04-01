using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Specification.Filters
{
    public interface IDataValueFilter : IDataItem
    {
        List<string> AddedValues { get; set; }
        bool AddedValuesSpecified { get; }
        List<string> RemovedValues { get; set; }
        bool RemovedValuesSpecified { get; }

        List<string> GetValues(List<string> allValues = null);
        bool IsValueAllowed(string value, List<string> allValues = null);
        void Repair(List<string> allValues = null);
    }
}