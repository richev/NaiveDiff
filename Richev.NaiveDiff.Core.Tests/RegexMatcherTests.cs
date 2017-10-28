using NUnit.Framework;
using Richev.NaiveDiff.Core.Matcher;

namespace Richev.NaiveDiff.Core.Tests
{
    [TestFixture]
    public class RegexMatcherTests
    {
        RegexMatcher _matcher;

        [SetUp]
        public void SetUp()
        {
            _matcher = new RegexMatcher(@"\d{1,2}\/\d{1,2}\/\d{4}");
        }

        [TestCase("identical", "identical")]
        [TestCase("Different Case", "DIFFERENT case")]
        [TestCase("28/10/2017 with date", "27/09/2016 with date")]
        public void Matches(string lineLeft, string lineRight)
        {
            var matches = _matcher.Matches(lineLeft, lineRight);

            Assert.IsTrue(matches);
        }

        [TestCase("not the", "same")]
        public void DoesNotMatch(string lineLeft, string lineRight)
        {
            var matches = _matcher.Matches(lineLeft, lineRight);

            Assert.IsFalse(matches);
        }
    }
}
