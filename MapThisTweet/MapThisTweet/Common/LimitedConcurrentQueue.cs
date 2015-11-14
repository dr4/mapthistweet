using System.Collections.Concurrent;

namespace MapThisTweet.Common
{
    public class LimitedConcurrentQueue<T> : ConcurrentQueue<T>
    {
        private const long maxSize = 20;

        private readonly object syncRoot = new object();

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            if(Count > maxSize)
            {
                lock(syncRoot)
                {
                    while(Count > maxSize)
                    {
                        T oldItem;
                        base.TryDequeue(out oldItem);
                    }
                }
            }
        }
    }
}