using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public IBdoDataType Repair(IBdoDataType poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();
        }

        return poco;
    }
}
