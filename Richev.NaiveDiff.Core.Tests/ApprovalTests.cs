using ApprovalTests;
using ApprovalTests.Reporters;
using Moq;
using NUnit.Framework;
using Richev.NaiveDiff.Core.Matcher;

namespace Richev.NaiveDiff.Core.Tests
{
    [UseReporter(typeof(NCrunchReporter))]
    [TestFixture]
    public class ApprovalTests
    {
        private Diff _diff;

        [SetUp]
        public void SetUp()
        {
            var matcher = new RegexMatcher(@"\d{1,2}\/\d{1,2}\/\d{4}");

            _diff = new Diff(matcher);
        }

        [Test]
        public void Foo()
        {
            var fileLeft = @"28/10/2017 Line A
28/10/2017 Line B";

            const string fileRight = @"27/10/2017  Line B
27/10/2017 Line C";

            var result = Utils.GetDiffResult(_diff, fileLeft, fileRight);

            var processed = Outputter.Process(result);

            Approvals.Verify(processed);
        }
    }
}
