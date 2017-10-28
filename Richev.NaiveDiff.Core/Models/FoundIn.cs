using System.Diagnostics;

namespace Richev.NaiveDiff.Core.Models
{
    /// <summary>
    /// Indicates in which file a line was found.
    /// </summary>
    public enum FoundIn
    {
        /// <summary>
        /// The line was found in both files.
        /// </summary>
        [DebuggerDisplay("=")]
        Both,

        /// <summary>
        /// The line was found only in the left file.
        /// </summary>
        [DebuggerDisplay("<")]
        LeftOnly,

        /// <summary>
        /// The line was found only in the right file.
        /// </summary>
        [DebuggerDisplay(">")]
        RightOnly
    }
}