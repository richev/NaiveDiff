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
        Both,

        /// <summary>
        /// The line was found only in the left file.
        /// </summary>
        LeftOnly,

        /// <summary>
        /// The line was found only in the right file.
        /// </summary>
        RightOnly
    }
}