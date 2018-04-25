using System.Diagnostics;

namespace Richev.NaiveDiff.Core.Models
{
    [DebuggerDisplay("{FoundIn} {Line}")]
    public class DiffLine
    {
        public DiffLine(FoundIn foundIn, string line)
        {
            FoundIn = foundIn;
            Line = line;
        }

        public FoundIn FoundIn { get; }

        public string Line { get; }
    }
}