using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Lifetime;

namespace Unity.Specification.Lifetime
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void ThenDisposingOfChildContainerDisposesOnlyChildObject()
        {
            var child1 = Container.CreateChildContainer();
            Container.RegisterType<TestClass>(new HierarchicalLifetimeManager());

            var o1 = Container.Resolve<TestClass>();
            var o2 = child1.Resolve<TestClass>();

            child1.Dispose();
            Assert.IsFalse(o1.Disposed);
            Assert.IsTrue(o2.Disposed);
        }
    }
}
