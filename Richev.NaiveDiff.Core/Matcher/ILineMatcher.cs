namespace Richev.RegexDiff.Core.Matcher
{
    public interface ILineMatcher
    {
        bool Matches(string lineLeft, string lineRight);
    }
}
