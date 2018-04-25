using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Richev.NaiveDiff.Core.Matcher;
using Richev.NaiveDiff.Core.Models;

namespace Richev.NaiveDiff.Core
{
    public class Diff : IDiff
    {
        private readonly ILineMatcher _lineMatcher;

        private StreamReader _streamLeft;
        private StreamReader _streamRight;

        public Diff(ILineMatcher lineMatcher)
        {
            _lineMatcher = lineMatcher;
        }

        public DiffResult GetDiff(Stream left, Stream right)
        {
            _streamLeft = new StreamReader(left);
            _streamRight = new StreamReader(right);

            var result = new DiffResult();

            var linesLeft = new Queue<string>();
            var linesRight = new Queue<string>();

            string lineLeft;
            string lineRight;

            while (!(_streamLeft.EndOfStream && _streamRight.EndOfStream))
            {
                lineLeft = _streamLeft.ReadLine();
                lineRight = _streamRight.ReadLine();

                if (_lineMatcher.Matches(lineLeft, lineRight))
                {
                    result.Add(FoundIn.Both, lineLeft);
                }
                else
                {
                    linesLeft.EnqueueUnlessNull(lineLeft);
                    linesRight.EnqueueUnlessNull(lineRight);

                    SeekUntilMatch(lineLeft, lineRight, linesLeft, linesRight);

                    if (MatchesLast(linesLeft, lineRight))
                    {
                        AddUnmatchedLines(linesLeft, FoundIn.LeftOnly, linesRight, result);
                    }
                    else if (MatchesLast(linesRight, lineLeft))
                    {
                        AddUnmatchedLines(linesRight, FoundIn.RightOnly, linesLeft, result);
                    }
                }
            }

            AddTrailingUnmatchedLines(linesLeft, FoundIn.LeftOnly, result);
            AddTrailingUnmatchedLines(linesRight, FoundIn.RightOnly, result);

            return result;
        }

        private void AddUnmatchedLines(Queue<string> lines1, FoundIn found, Queue<string> lines2, DiffResult result)
        {
            if (!_lineMatcher.Matches(lines1.Last(), lines2.First()))
            {
                throw new InvalidOperationException($"'{lines1.Last()}' and '{lines2.First()}' don't match, but should.");
            }

            while (lines1.Count > 1)
            {
                result.Add(found, lines1.Dequeue());
            }

            result.Add(FoundIn.Both, lines1.Dequeue());
            lines2.Dequeue();
        }

        private void AddTrailingUnmatchedLines(Queue<string> lines, FoundIn found, DiffResult result)
        {
            while (lines.Any())
            {
                result.Add(found, lines.Dequeue());
            }
        }

        private bool MatchesLast(Queue<string> lines, string line)
        {
            return lines.Any() && _lineMatcher.Matches(lines.Last(), line);
        }

        private void SeekUntilMatch(
            string lineLeft,
            string lineRight,
            Queue<string> linesLeft,
            Queue<string> linesRight)
        {
            while (linesLeft?.LastOrDefault() != lineRight &&
                linesRight?.LastOrDefault() != lineLeft &&
                !(_streamLeft.EndOfStream && _streamRight.EndOfStream))
            {
                linesLeft.EnqueueUnlessNull(_streamLeft.ReadLine());
                linesRight.EnqueueUnlessNull(_streamRight.ReadLine());
            }
        }

        public void Dispose()
        {
            _streamLeft?.Dispose();
            _streamRight?.Dispose();
        }
    }
}
