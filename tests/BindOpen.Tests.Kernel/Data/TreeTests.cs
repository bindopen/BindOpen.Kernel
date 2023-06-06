using BindOpen.Data;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests.Data
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
                var child = _tree.InsertChild(q => q.WithName("Child" + i));

                for (var j = 0; j < 10; j++)
                {
                    child.InsertChild(q => q.WithName("Sub child" + j));
                }
            }
        }

        [Test, Order(1)]
        public void CreateTree()
        {
            var name = _tree.Children()?.FirstOrDefault()?.Name;
            Assert.That(name == "Child0", "Bad data source creation");

            var count = _tree.Children()?.Count();
            Assert.That(count == 110, "Bad data source creation");
        }
    }
}
