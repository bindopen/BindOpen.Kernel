﻿using BindOpen.Scoping.Scopes;
using NUnit.Framework;

namespace BindOpen.Tests.Scoping.Scopes
{
    [TestFixture, Order(300)]
    public class BdoScopeTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateScopeNewObjectTest()
        {
            var scope = BdoScopes.NewScope()
                .LoadExtensions(
                    q => q.AddAssemblyFrom<GlobalSetUp>());
        }
    }

}