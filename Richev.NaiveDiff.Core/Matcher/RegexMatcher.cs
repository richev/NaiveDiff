using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Richev.NaiveDiff.Core.Matcher
{
    public class RegexMatcher : ILineMatcher
    {
        private readonly IList<Regex> _regex;

        public RegexMatcher(params string[] regexPatterns)
        {
            _regex = new List<Regex>();

            foreach (var pattern in regexPatterns)
            {
                _regex.Add(new Regex(pattern, RegexOptions.CultureInvariant | RegexOptions.Compiled));
            }
        }

        public bool Matches(string lineLeft, string lineRight)
        {
            foreach (var regex in _regex)
            {
                lineLeft = regex.Replace(lineLeft, string.Empty);
                lineRight = regex.Replace(lineRight, string.Empty);
            }

            return lineLeft.Equals(lineRight, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
