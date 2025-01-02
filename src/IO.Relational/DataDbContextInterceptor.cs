using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BindOpen.Data;

public class DataDbContextInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        //if (eventData.Context is null) return result;

        //int i = 0;
        //var entries = eventData.Context.ChangeTracker.Entries();

        //while (i < entries.Count())
        //{
        //    var entry = entries[i];

        //    if (entry is not { State: EntityState.Deleted, Entity: IBdoDb deleted }) continue;

        //    foreach (var navigationEntry in entry.Navigations)
        //    {
        //        if (navigationEntry is CollectionEntry collectionEntry)
        //        {
        //            //navigationEntry.EntityEntry.State = EntityState.Deleted;
        //            foreach (var dependentEntry in collectionEntry.CurrentValue)
        //            {
        //                eventData.Context.Remove(dependentEntry);
        //            }
        //        }
        //    }
        //}
        return result;
    }
}