﻿using System;
using System.Linq;
using Fun.Linq;
using NUnit.Framework;
using Shouldly;

namespace Fun.Tests
{
    [TestFixture]
    public class TryQueryExpressionTests
    {
        #region Select

        [Test]
        public void FromSelect_ShouldMapInputValue()
        {
            var result = from x in Try.Some(1)
                         select x.ToString();

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe("1");
        }

        [Test]
        public void FromSelect_ShouldPassInputError()
        {
            var result = from x in Try.Error<int>(new Exception())
                         select x.ToString();

            result.HasValue.ShouldBeFalse();
        }

        #endregion

        #region SelectMany

        [Test]
        public void FromFromSelect_ShouldMapInputValues()
        {
            var result = from x in Try.Some(1)
                         from y in Try.Some(2)
                         select x + y;

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe(3);
        }

        [Test]
        public void FromFromSelect_ShouldPassFirstError()
        {
            var result = from x in Try.Error<int>(new Exception())
                         from y in Try.Some(2)
                         select x + y;

            result.HasValue.ShouldBeFalse();
        }

        [Test]
        public void FromFromSelect_ShouldPassSecondError()
        {
            var result = from x in Try.Some(1)
                         from y in Try.Error<int>(new Exception())
                         select x + y;

            result.HasValue.ShouldBeFalse();
        }

        [Test]
        public void ManyFroms_ShouldMapInputValues()
        {
            var result = from w in Try.Some(1)
                         from x in Try.Some(2)
                         from y in Try.Some(3)
                         from z in Try.Some(2)
                         select (w + x + y + z) / 4;

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe(2);
        }

        #endregion

        #region Where

        #endregion
    }
}
