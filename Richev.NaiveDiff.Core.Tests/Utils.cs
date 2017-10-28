using System;
using System.IO;
using Richev.NaiveDiff.Core.Models;

namespace Richev.NaiveDiff.Core.Tests
{
    public static class Utils
    {
        public static DiffResult GetDiffResult(Diff diff, string[] linesLeft, string[] linesRight)
        {
            Stream GetStream(string s)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(s);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }

            using (var streamLeft = GetStream(string.Join(Environment.NewLine, linesLeft)))
            using (var streamRight = GetStream(string.Join(Environment.NewLine, linesRight)))
            {
                return diff.GetDiff(streamLeft, streamRight);
            }
        }

        public static DiffResult GetDiffResult(Diff diff, string fileLeft, string fileRight)
        {
            Stream GetStream(string s)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(s);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }

            using (var streamLeft = GetStream(fileLeft))
            using (var streamRight = GetStream(fileRight))
            {
                return diff.GetDiff(streamLeft, streamRight);
            }
        }
    }
}
