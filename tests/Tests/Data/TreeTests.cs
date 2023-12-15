using BindOpen.Kernel.Tests;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Kernel.Data
{
    [TestFixture, Order(210)]
    public class TreeTests
    {
        private TreeFake _tree;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _tree = new TreeFake();
            for (var i = 0; i < 10; i++)
            {
                var child = _tree.InsertChild<TreeFake>(q => q.WithName("child" + i));

                for (var j = 0; j < 10; j++)
                {
                    child.InsertChild<TreeFake>(q => q.WithName("subChild" + j));
                }
            }
        }

        [Test, Order(1)]
        public void ChildrenTest()
        {
            var name = _tree.Children()?.FirstOrDefault()?.Name;
            Assert.That(name == "child0", "Bad data source creation");

            var count = _tree.Children()?.Count();
            Assert.That(count == 10, "Bad data source creation");

            count = _tree.Children(true)?.Count();
            Assert.That(count == 110, "Bad data source creation");
        }

        [Test, Order(1)]
        public void HasChildTest()
        {
            var found = _tree.HasChild();
            Assert.That(found, "Bad data source creation");

            found = _tree.HasChild(q => q.Name == "child9");
            Assert.That(found, "Bad data source creation");

            found = _tree.HasChild(q => q.Name == "subChild9");
            Assert.That(!found, "Bad data source creation");

            found = _tree.HasChild(q => q.Name == "subChild9", true);
            Assert.That(found, "Bad data source creation");
        }

        [Test, Order(1)]
        public void ChildTest()
        {
            var child = _tree.FirstChild();
            Assert.That(child != null, "Bad data source creation");

            child = _tree.Child(q => q.Name == "child9");
            Assert.That(child != null, "Bad data source creation");

            child = _tree.Child(q => q.Name == "subChild9");
            Assert.That(child == null, "Bad data source creation");

            child = _tree.Child(q => q.Name == "subChild9", true);
            Assert.That(child != null, "Bad data source creation");
        }
    }
}
