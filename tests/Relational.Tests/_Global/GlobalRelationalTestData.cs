using BindOpen.Data;

namespace BindOpen;

public static class GlobalRelationalTestData
{
    /// <summary>
    /// 
    /// </summary>
    public static string GetDbFilePath()
    {
        return DataTestData.WorkingFolder + "database.db";
    }

    /// <summary>
    /// 
    /// </summary>
    public static SqliteDataDbContext CreateDbContext()
    {
        var dbFilePath = GetDbFilePath();
        var dbContext = new SqliteDataDbContext($"Data Source={dbFilePath};");

        return dbContext;
    }
}
