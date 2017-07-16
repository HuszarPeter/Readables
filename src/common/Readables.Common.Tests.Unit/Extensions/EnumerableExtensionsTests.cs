using NUnit.Framework;
using Readables.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Readables.Common.Tests.Unit.Extensions
{
    [TestFixture]
    public static class EnumerableExtensionsTests
    {
        [Test]
        public static void ShouldThrowException_When_EnumerableIsNull()
        {
            IEnumerable<string> enumerable = null;
            Action act = () => enumerable.ForEach((item) => { });
            act.ShouldThrow<ArgumentNullException>().Which.ParamName.Should().Be("enumerable");
        }

        [Test]
        public static void ShouldThrowExcetion_When_ActionIsNull()
        {
            IEnumerable<string> enumerable = new List<string>
            {
                "item1",
                "item2"
            };
            Action act = () => enumerable.ForEach(null);
            act.ShouldThrow<ArgumentNullException>().Which.ParamName.Should().Be("action");
        }
    }
}
