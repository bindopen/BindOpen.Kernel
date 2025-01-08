using BindOpen.Data.Helpers;
using Bogus;
using DeepEqual.Syntax;
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
        IBdoMerger exp1,
        IBdoMerger exp2)
    {
        if ((exp1 != null && exp2 == null) || (exp1 == null && exp2 != null))
        {
            Assert.That(Equals(exp1, exp2), "Unmatched objects");
        }

        var deepEq = exp1.WithDeepEqual(exp2);

        if (exp1?.AddedValues?.Any() != true && exp2?.AddedValues?.Any() != true)
        {
            deepEq.IgnoreProperty<IBdoMerger>(x => x.AddedValues);
        }
        if (exp1?.RemovedValues?.Any() != true && exp2?.RemovedValues?.Any() != true)
        {
            deepEq.IgnoreProperty<IBdoMerger>(x => x.RemovedValues);
        }
        deepEq.Assert();
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
