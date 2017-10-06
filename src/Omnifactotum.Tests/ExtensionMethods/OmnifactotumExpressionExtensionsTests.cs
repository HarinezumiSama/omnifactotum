﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;

namespace Omnifactotum.Tests.ExtensionMethods
{
    [TestFixture]
    public sealed class OmnifactotumExpressionExtensionsTests
    {
        [Test]
        public void TestGetLastMethodLastIsToString()
        {
            Expression<Func<OmnifactotumExpressionExtensionsTests, string>> expression = obj => obj.ToString();
            var method = expression.GetLastMethod();
            Assert.That(method, Is.Not.Null);
            Assert.That(method.Name, Is.EqualTo("ToString"));
            Assert.That(method.DeclaringType, Is.EqualTo(typeof(object)));
        }

        [Test]
        public void TestGetLastMethodLastIsSelf()
        {
            Expression<Action<OmnifactotumExpressionExtensionsTests>> expression =
                obj => obj.TestGetLastMethodLastIsSelf();
            var method = expression.GetLastMethod();
            Assert.That(method, Is.Not.Null);
            Assert.That(method.Name, Is.EqualTo(MethodBase.GetCurrentMethod().Name));
            Assert.That(method.DeclaringType, Is.EqualTo(GetType()));
        }

        [Test]
        public void TestGetLastMethodSeveralCallsAndLastIsIndexOf()
        {
            Expression<Func<OmnifactotumExpressionExtensionsTests, int>> expression =
                obj => obj.ToString().IndexOf("1", StringComparison.Ordinal);
            var method = expression.GetLastMethod();
            Assert.That(method, Is.Not.Null);
            Assert.That(method.Name, Is.EqualTo("IndexOf"));
            Assert.That(method.DeclaringType, Is.EqualTo(typeof(string)));
        }

        [Test]
        public void TestGetLastMethodLastIsProperty()
        {
            Expression<Func<OmnifactotumExpressionExtensionsTests, int>> expression = obj => obj.ToString().Length;
            var method = expression.GetLastMethod();
            Assert.That(method, Is.Null);
        }
    }
}