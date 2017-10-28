using System.Collections.Generic;

namespace Richev.NaiveDiff.Core.Models
{
    public class DiffResult
    {
        public void Add(FoundIn found, string line)
        {
            _lines.Add(new DiffLine(found, line));
        }

        private readonly List<DiffLine> _lines = new List<DiffLine>();

        public IEnumerable<DiffLine> Lines { get { return _lines; } }
    }
}