using System.Collections.Concurrent;

namespace MapThisTweet.Common
{
    public class LimitedConcurrentQueue<T> : ConcurrentQueue<T>
    {
        private const long MaxSize = 20;

        private readonly object syncRoot = new object();

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            if(Count > MaxSize)
            {
                lock(syncRoot)
                {
                    while(Count > MaxSize)
                    {
                        T oldItem;
                        base.TryDequeue(out oldItem);
                    }
                }
            }
        }
    }
}