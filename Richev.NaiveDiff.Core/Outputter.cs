using System;
using System.Text;
using Richev.NaiveDiff.Core.Models;

namespace Richev.NaiveDiff.Core
{
    public static class Outputter
    {
        public static string Process(DiffResult result)
        {
            var sb = new StringBuilder();

            foreach(var line in result.Lines)
            {
                sb.AppendLine($"{FoundInToString(line.FoundIn)} {line.Line}");
            }

            return sb.ToString();
        }

        private static string FoundInToString(FoundIn foundIn)
        {
            switch (foundIn)
            {
                case FoundIn.Both:
                    return "=";
                case FoundIn.LeftOnly:
                    return "<";
                case FoundIn.RightOnly:
                    return ">";
                default:
                    throw new ArgumentOutOfRangeException(nameof(foundIn), $"{foundIn} is not a valid value for type {foundIn.GetType().Name}.");
            }
        }
    }
}
