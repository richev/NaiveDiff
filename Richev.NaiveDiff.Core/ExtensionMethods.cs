using System.Collections.Generic;

namespace Richev.NaiveDiff.Core
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Adds the object to the end of the <see cref="System.Collections.Generic.Queue{T}"/>, unless the object is null.
        /// </summary>
        /// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
        /// <param name="queue">The queue.</param>
        /// <param name="value">The object to add to the <see cref="System.Collections.Generic.Queue{T}"/>.</param>
        public static void EnqueueUnlessNull<T>(this Queue<T> queue, T value) where T : class
        {
            if (value != null)
            {
                queue.Enqueue(value);
            }
        }
    }
}
