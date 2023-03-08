using BindOpen.Data;
using BindOpen.Data.Helpers;
using Bogus;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Tests.Kernel.Data
{
    [TestFixture, Order(210)]
    public class BdoStringFilterTests
    {
        List<string> _addList = new();
        List<string> _removeList = new();
        List<string> _list = new();

        public BdoStringFilter _filter = null;


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
            var filter = BdoData.NewStringFilter(_addList, _removeList);
            var newList = filter.GetValues(list);

            Assert.That(
                newList.All(q => _list?.Contains(q) == true || _addList?.Contains(q) == true), "Error with string filter");
            Assert.That(
                _removeList.All(q => newList?.Contains(q) == false), "Error with string filter");
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            var list = _list.Adding(_removeList?.ToArray());
            var filter = BdoData.NewStringFilter().Removing(_removeList?.ToArray());
            var newList = filter.GetValues(list);

            Assert.That(
                newList.All(q => list?.Contains(q) == true), "Error with string filter");
            Assert.That(
                newList.All(q => _removeList?.Contains(q) == false), "Error with string filter");
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            var list = _list.Adding(_removeList?.ToArray());
            var filter = BdoData.NewStringFilter().Adding(_addList?.ToArray());
            var newList = filter.GetValues();

            Assert.That(
                newList.All(q => _addList?.Contains(q) == true), "Error with string filter");
            Assert.That(
                _addList.All(q => newList?.Contains(q) == true || _removeList?.Contains(q) == true), "Error with string filter");
        }
    }
}
