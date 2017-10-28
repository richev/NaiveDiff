using ApprovalTests;
using ApprovalTests.Reporters;
using Moq;
using NUnit.Framework;
using Richev.NaiveDiff.Core.Matcher;

namespace Richev.NaiveDiff.Core.Tests
{
    [UseReporter(typeof(WinMergeReporter))]
    [TestFixture]
    public class ApprovalTests
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

        [Test]
        public void Foo()
        {
            var fileLeft = @"Line A
Line B";

            const string fileRight = @"Line B
Line C";

            var result = Utils.GetDiffResult(_diff, fileLeft, fileRight);

            var processed = Outputter.Process(result);

            Approvals.Verify(processed);
        }
    }
}
