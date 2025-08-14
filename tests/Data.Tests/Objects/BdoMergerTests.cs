using BindOpen.Data.Helpers;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoMergerTests
{
    List<string> _addList = [];
    List<string> _removeList = [];
    List<string> _list = [];

    public BdoMerger _merger = null;


    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();
        _addList = f.Random.WordsArray(5, 10)?.ToList();
        _removeList = f.Random.WordsArray(5, 10)?.ToList();
        _list = f.Random.WordsArray(5, 10)?.ToList();
    }

    public static void AssertEquals(
        IBdoMerger merger1,
        IBdoMerger merger2)
    {
        if ((merger1 != null && merger2 == null) || (merger1 == null && merger2 != null))
        {
            Assert.That(Equals(merger1, merger2), "Unmatched objects");
        }

        merger1.Should().BeEquivalentTo(
            merger2,
            options =>
            {
                if (merger1?.AddedValues?.Any() != true && merger2?.AddedValues?.Any() != true)
                {
                    options = options.Excluding(x => x.AddedValues);
                }
                if (merger1?.RemovedValues?.Any() != true && merger2?.RemovedValues?.Any() != true)
                {
                    options = options.Excluding(x => x.RemovedValues);
                }

                return options;
            });
    }

    [Test, Order(1)]
    public void Create1Test()
    {
        var list = _list.Adding(_removeList?.ToArray());
        _merger = BdoData.NewMerger(_addList, _removeList);
        var newList = list.Merge(_merger);

        Assert.That(
            _addList.All(q => newList?.Contains(q) == true || _removeList?.Contains(q) == true), "Error with string set");
        Assert.That(
            newList.All(q => _removeList?.Contains(q) == false), "Error with string set");
    }

    [Test, Order(2)]
    public void Create2Test()
    {
        var list = _list.Adding(_removeList?.ToArray());
        _merger = BdoData.NewMerger()
            .Removing(_removeList?.ToArray());
        var newList = list.Merge(_merger);

        Assert.That(
            newList.All(q => _removeList?.Contains(q) == false), "Error with string set");
    }

    [Test, Order(3)]
    public void Create3Test()
    {
        var list = _list.Adding(_removeList?.ToArray());
        _merger = BdoData.NewMerger()
            .Adding(_addList?.ToArray())
            .Removing(_removeList?.ToArray());
        var newList = list.Merge(_merger);

        Assert.That(
            _addList.All(q => newList?.Contains(q) == true || _removeList?.Contains(q) == true), "Error with string set");
        Assert.That(
            newList.All(q => _removeList?.Contains(q) == false), "Error with string set");
    }
}
