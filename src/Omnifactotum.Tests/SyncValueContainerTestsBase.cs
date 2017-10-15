﻿using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Omnifactotum.NUnit;

//// ReSharper disable AssignNullToNotNullAttribute - For negative test cases

namespace Omnifactotum.Tests
{
    internal abstract class SyncValueContainerTestsBase<T>
        where T : IEquatable<T>
    {
        private readonly T _value;
        private readonly T _anotherValue;
        private readonly T[] _values;

        protected SyncValueContainerTestsBase(T value, T anotherValue)
        {
            Assert.That(value, Is.Not.EqualTo(default(T)));

            _value = value;
            _anotherValue = anotherValue;
            _values = new[] { value, anotherValue, default(T) };
        }

        [Test]
        public void TestSupportedInterfaces()
        {
            Assert.That(typeof(IValueContainer<T>).IsAssignableFrom(typeof(SyncValueContainer<T>)), Is.True);
        }

        [Test]
        public void TestPropertyAccess()
        {
            NUnitFactotum.For<SyncValueContainer<T>>.AssertReadableWritable(
                obj => obj.SyncObject,
                PropertyAccessMode.ReadOnly);

            NUnitFactotum.For<SyncValueContainer<T>>.AssertReadableWritable(
                obj => obj.Value,
                PropertyAccessMode.ReadWrite);
        }

        [Test]
        public void TestConstructionDefault()
        {
            var container = new SyncValueContainer<T>();

            Assert.That(container.SyncObject, Is.Not.Null & Is.TypeOf<object>());

            Assert.That(
                container.Value,
                typeof(T).IsValueType ? (IResolveConstraint)Is.EqualTo(default(T)) : Is.Null);
        }

        [Test]
        public void TestConstructionWithValue()
        {
            foreach (var value in _values)
            {
                var container = new SyncValueContainer<T>(value);

                Assert.That(container.SyncObject, Is.Not.Null & Is.TypeOf<object>());

                Assert.That(
                    container.Value,
                    typeof(T).IsValueType ? (IResolveConstraint)Is.EqualTo(value) : Is.SameAs(value));
            }
        }

        [Test]
        public void TestConstructionWithValueAndSyncObject()
        {
            foreach (var value in _values)
            {
                var syncObject = new object();
                var container = new SyncValueContainer<T>(value, syncObject);

                Assert.That(container.SyncObject, Is.Not.Null & Is.SameAs(syncObject));

                Assert.That(
                    container.Value,
                    typeof(T).IsValueType ? (IResolveConstraint)Is.EqualTo(value) : Is.SameAs(value));
            }
        }

        [Test]
        public void TestConstructionWithValueAndSyncObjectNegative()
        {
            Assert.That(() => new SyncValueContainer<T>(_value, null), Throws.TypeOf<ArgumentNullException>());

            Assert.That(() => new SyncValueContainer<T>(_value, 123), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestValue()
        {
            var container = new SyncValueContainer<T>(_value);

            Assert.That(
                container.Value,
                typeof(T).IsValueType ? (IResolveConstraint)Is.EqualTo(_value) : Is.SameAs(_value));

            container.Value = _anotherValue;

            Assert.That(
                container.Value,
                typeof(T).IsValueType ? (IResolveConstraint)Is.EqualTo(_anotherValue) : Is.SameAs(_anotherValue));
        }
    }
}