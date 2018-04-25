namespace Richev.NaiveDiff.Core.Matcher
{
    public interface ILineMatcher
    {
        /// <summary>
        /// Returns true if the lines match.
        /// </summary>
        bool Matches(string lineLeft, string lineRight);
    }
}
