﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Parameter.Optional
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            Container.RegisterInstance("other");
            Container.RegisterInstance(Name, Name);
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
        }

        #region Test Data

        public class Service
        {
            #region Properties

            public object Value { get; private set; }

            public object ValueOne { get; private set; }

            public object ValueTwo { get; private set; }

            #endregion

            [InjectionMethod]
            public void Method(object value)
            {
                Value = value;
            }

            public void MethodOne(string value)
            {
                ValueOne = value;
            }

            public void MethodTwo(object one, object two)
            {
                ValueOne = one;
                ValueTwo = two;
            }
        }

        // Our generic interface 
        public interface ICommand<T>
        {
            T Executed { get; }

            ICommand<T> Chained { get; }

            void Execute(T data);

            void ChainedExecute(ICommand<T> inner);
        }

        // An implementation of ICommand that executes them.
        public class ConcreteCommand<T> : ICommand<T>
        {
            public T Executed { get; private set; }

            public ICommand<T> Chained { get; private set; }


            public void Execute(T data)
            {
                Executed = data;
            }

            public void ChainedExecute(ICommand<T> inner)
            {
                Chained = inner;
            }
        }

        // And a decorator implementation that wraps an Inner ICommand<>
        public class LoggingCommand<T> : ICommand<T>
        {
            private ICommand<T> inner;

            public bool ChainedExecuteWasCalled = false;
            public bool WasInjected = false;

            public LoggingCommand(ICommand<T> inner)
            {
                this.inner = inner;
            }

            public LoggingCommand()
            {
            }

            public T Executed { get; private set; }

            public ICommand<T> Chained { get; private set; }


            public ICommand<T> Inner
            {
                get { return inner; }
                set { inner = value; }
            }

            public void Execute(T data)
            {
                // do logging here
                Inner.Execute(data);
                Executed = data;
            }

            public void ChainedExecute(ICommand<T> innerCommand)
            {
                ChainedExecuteWasCalled = true;
                Chained = innerCommand;
            }

            public void InjectMe()
            {
                WasInjected = true;
            }
        }


        // A couple of sample objects we're stuffing into our commands
        public class User
        {
            public void DoSomething(string message)
            {
            }
        }

        public class Account
        {
        }

        // InjectionParameterValue type used for testing nesting
        public struct Customer
        {
        }

        #endregion
    }
}
