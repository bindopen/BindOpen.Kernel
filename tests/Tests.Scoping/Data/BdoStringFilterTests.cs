using BindOpen.System.Data;
using BindOpen.System.Data.Helpers;
using Bogus;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Tests.Scoping.Data
{
    [TestFixture, Order(210)]
    public class BdoStringSetTests
    {
        List<string> _addList = new();
        List<string> _removeList = new();
        List<string> _list = new();

        public BdoStringSet _set = null;


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
            var set = BdoData.NewStringSet(_addList, _removeList);
            var newList = list.Merge(set);

            Assert.That(
                newList.All(q => _list?.Contains(q) == true || _addList?.Contains(q) == true), "Error with string set");
            Assert.That(
                _addList.All(q => newList?.Contains(q) == true), "Error with string set");
            Assert.That(
                _addList.All(q => newList?.Contains(q) == true || _removeList?.Contains(q) == true), "Error with string set");
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            var list = _list.Adding(_removeList?.ToArray());
            var set = BdoData.NewStringSet()
                .Removing(_removeList?.ToArray());
            var newList = list.Merge(set);

            Assert.That(
                newList.All(q => list?.Contains(q) == true), "Error with string set");
            Assert.That(
                newList.All(q => _removeList?.Contains(q) == false), "Error with string set");
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            var list = _list.Adding(_removeList?.ToArray());
            var set = BdoData.NewStringSet()
                .Adding(_addList?.ToArray())
                .Removing(_removeList?.ToArray());
            var newList = list.Merge(set);

            Assert.That(
                newList.All(q => _list?.Contains(q) == true || _addList?.Contains(q) == true), "Error with string set");
            Assert.That(
                _addList.All(q => newList?.Contains(q) == true && !_removeList?.Contains(q) == true), "Error with string set");
        }
    }
}
