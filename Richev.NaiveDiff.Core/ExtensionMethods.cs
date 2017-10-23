using System.Collections.Generic;

namespace Richev.RegexDiff.Core
{
    public static class ExtensionMethods
    {
        public static void EnqueueUnlessNull<T>(this Queue<T> queue, T value) where T : class
        {
            if (value != null)
            {
                queue.Enqueue(value);
            }
        }
    }
}
