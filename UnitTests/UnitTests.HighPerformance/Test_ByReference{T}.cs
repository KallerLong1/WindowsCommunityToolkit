﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using Microsoft.Toolkit.HighPerformance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.HighPerformance.Extensions
{
    [TestClass]
    public class Test_ByReferenceOfT
    {
        [TestCategory("ByReferenceOfT")]
        [TestMethod]
#if NETCOREAPP2_1
        public void Test_ByReferenceOfT_CreateByReferenceOfT()
        {
            var model = new FieldOwner { Value = 1 };
            var reference = new ByReference<int>(model, ref model.Value);

            Assert.IsTrue(Unsafe.AreSame(ref model.Value, ref reference.Value));

            reference.Value++;

            Assert.AreEqual(model.Value, 2);
        }

        /// <summary>
        /// A dummy model that owns an <see cref="int"/> field.
        /// </summary>
        private sealed class FieldOwner
        {
            public int Value;
        }
#else
        public void Test_ByReferenceOfT_CreateByReferenceOfT()
        {
            int value = 1;
            var reference = new ByReference<int>(ref value);

            Assert.IsTrue(Unsafe.AreSame(ref value, ref reference.Value));

            reference.Value++;

            Assert.AreEqual(value, 2);

        }
#endif
    }
}
