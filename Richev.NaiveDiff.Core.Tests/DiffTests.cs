using System.Linq;
using Moq;
using NUnit.Framework;
using Richev.NaiveDiff.Core.Matcher;
using Richev.NaiveDiff.Core.Models;

namespace Richev.NaiveDiff.Core.Tests
{
    [TestFixture]
    public class DiffTests
    {
        private Diff _diff;

        [SetUp]
        public void SetUp()
        {
            var lineMatcherMock = new Mock<ILineMatcher>();
            lineMatcherMock
                .Setup(m => m.Matches(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((lineLeft, lineRight) => lineLeft == lineRight);

            _diff = new Diff(lineMatcherMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _diff.Dispose();
        }

        [Test]
        public void Given_matching_lines_returns_expected()
        {
            var result = Utils.GetDiffResult(
                _diff,
                new[] { "foo", "bar" },
                new[] { "foo", "bar" });

            Assert.IsNotNull(result);
            var lines = result.Lines.ToList();
            Assert.AreEqual(2, lines.Count);
            AssertLine(FoundIn.Both, "foo", lines[0]);
            AssertLine(FoundIn.Both, "bar", lines[1]);
        }

        [Test]
        public void Given_unmatched_last_line_in_left_returns_expected()
        {
            var result = Utils.GetDiffResult(
                _diff,
                new[] { "foo", "bar" },
                new[] { "foo" });

            Assert.IsNotNull(result);
            var lines = result.Lines.ToList();
            Assert.AreEqual(2, lines.Count);
            AssertLine(FoundIn.Both, "foo", lines[0]);
            AssertLine(FoundIn.LeftOnly, "bar", lines[1]);
        }

        [Test]
        public void Given_unmatched_last_line_in_right_returns_expected()
        {
            var result = Utils.GetDiffResult(
                _diff,
                new[] { "foo" },
                new[] { "foo", "bar" });

            Assert.IsNotNull(result);
            var lines = result.Lines.ToList();
            Assert.AreEqual(2, lines.Count);
            AssertLine(FoundIn.Both, "foo", lines[0]);
            AssertLine(FoundIn.RightOnly, "bar", lines[1]);
        }

        [Test]
        public void Given_unmatched_last_line_in_both_returns_expected()
        {
            var result = Utils.GetDiffResult(
                _diff,
                new[] { "foo", "bar"},
                new[] { "foo", "baz" });

            Assert.IsNotNull(result);
            var lines = result.Lines.ToList();
            Assert.AreEqual(3, lines.Count);
            AssertLine(FoundIn.Both, "foo", lines[0]);
            AssertLine(FoundIn.LeftOnly, "bar", lines[1]);
            AssertLine(FoundIn.RightOnly, "baz", lines[2]);
        }

        [Test]
        public void Given_unmatched_first_line_returns_expected()
        {
            var result = Utils.GetDiffResult(
                _diff,
                new[] { "foo", "bar" },
                new[] {        "bar" });

            Assert.IsNotNull(result);
            var lines = result.Lines.ToList();
            Assert.AreEqual(2, lines.Count);
            AssertLine(FoundIn.LeftOnly, "foo", lines[0]);
            AssertLine(FoundIn.Both, "bar", lines[1]);
        }

        [Test]
        public void Given_various_diffs_returns_expected()
        {
            var result = Utils.GetDiffResult(
                _diff,
                new[] { "a", "b", "c" },
                new[] { "a",      "c", "d" });

            Assert.IsNotNull(result);
            var lines = result.Lines.ToList();
            Assert.AreEqual(4, lines.Count);
            AssertLine(FoundIn.Both, "a", lines[0]);
            AssertLine(FoundIn.LeftOnly, "b", lines[1]);
            AssertLine(FoundIn.Both, "c", lines[2]);
            AssertLine(FoundIn.RightOnly, "d", lines[3]);
        }

        [Test]
        public void Given_missing_lines_returns_expected()
        {
            var result = Utils.GetDiffResult(
                _diff,
                new[] { "a",           "d" },
                new[] { "a", "b", "c", "d" });

            Assert.IsNotNull(result);
            var lines = result.Lines.ToList();
            Assert.AreEqual(4, lines.Count);
            AssertLine(FoundIn.Both, "a", lines[0]);
            AssertLine(FoundIn.RightOnly, "b", lines[1]);
            AssertLine(FoundIn.RightOnly, "c", lines[2]);
            AssertLine(FoundIn.Both, "d", lines[3]);
        }

        private static void AssertLine(FoundIn expectedFound, string expectedLine, DiffLine diffLine)
        {
            Assert.AreEqual(expectedFound, diffLine.FoundIn);
            Assert.AreEqual(expectedLine, diffLine.Line);
        }
    }
}
