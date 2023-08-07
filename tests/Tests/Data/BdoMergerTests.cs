using BindOpen.System.Data.Helpers;
using Bogus;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data
{
    [TestFixture, Order(210)]
    public class BdoMergerTests
    {
        List<string> _addList = new();
        List<string> _removeList = new();
        List<string> _list = new();

        public BdoMerger _set = null;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _addList = f.Random.WordsArray(5, 10)?.ToList();
            _removeList = f.Random.WordsArray(5, 10)?.ToList();
            _list = f.Random.WordsArray(5, 10)?.ToList();
        }

        [Test, Order(1)]
        public void Create1Test()
        {
            var list = _list.Adding(_removeList?.ToArray());
            var merger = BdoData.NewMerger(_addList, _removeList);
            var newList = list.Merge(merger);

            Assert.That(
                _addList.All(q => newList?.Contains(q) == true || _removeList?.Contains(q) == true), "Error with string set");
            Assert.That(
                newList.All(q => _removeList?.Contains(q) == false), "Error with string set");
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            var list = _list.Adding(_removeList?.ToArray());
            var merger = BdoData.NewMerger()
                .Removing(_removeList?.ToArray());
            var newList = list.Merge(merger);

            Assert.That(
                newList.All(q => _removeList?.Contains(q) == false), "Error with string set");
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            var list = _list.Adding(_removeList?.ToArray());
            var set = BdoData.NewMerger()
                .Adding(_addList?.ToArray())
                .Removing(_removeList?.ToArray());
            var newList = list.Merge(set);

            Assert.That(
                _addList.All(q => newList?.Contains(q) == true || _removeList?.Contains(q) == true), "Error with string set");
            Assert.That(
                newList.All(q => _removeList?.Contains(q) == false), "Error with string set");
        }
    }
}
