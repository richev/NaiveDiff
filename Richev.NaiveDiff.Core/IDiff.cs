using System;
using System.IO;
using Richev.RegexDiff.Core.Models;

namespace Richev.RegexDiff.Core
{
    public interface IDiff : IDisposable
    {
        DiffResult GetDiff(Stream left, Stream right);
    }
}