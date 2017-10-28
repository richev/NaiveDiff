using System;
using System.IO;
using Richev.NaiveDiff.Core.Models;

namespace Richev.NaiveDiff.Core
{
    public interface IDiff : IDisposable
    {
        DiffResult GetDiff(Stream left, Stream right);
    }
}