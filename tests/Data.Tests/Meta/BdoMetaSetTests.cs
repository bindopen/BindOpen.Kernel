using Bogus;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BindOpen.Data.Meta;

[TestFixture, Order(210)]
public class BdoMetaSetTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();
    }

    [Test, Order(1)]
    public async Task Create1Test()
    {
        var set = BdoData.NewSet(BdoData.NewScalar("path", "<mypath>"));

        var b = false;
        set.Invoke(q => q.Has("path"), () => { b = true; });
        Assert.That(b, "Error with Invoke method");

        b = false;
        await set.InvokeAsync(q => q.Has("path"), () => { b = true; return Task.CompletedTask; });
        Assert.That(b, "Error with Invoke method");
    }
}
