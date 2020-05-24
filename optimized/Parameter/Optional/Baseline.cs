﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Optional
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void Baseline()
        {
            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNull(result.ValueOne);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));

            Assert.IsNotNull(Container.Resolve<int>());
            Assert.IsNotNull(Container.Resolve<int>("1"));

            Assert.IsNotNull(Container.Resolve<string>());
            Assert.IsNotNull(Container.Resolve<string>("1"));
        }

        [TestMethod]
        public void AttributedMethodBaseline()
        {
            // Arrange
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.Method)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }
    }
}
