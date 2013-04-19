using System.Collections.Generic;
using System.Threading;

namespace AsteroidsEvolved.Threading
{
	class ConcurrentMultiqueue<T>
	{
		Queue<T> queue = new Queue<T>();
		Mutex mutex = new Mutex();


		public T enqueue(T val)
		{
			mutex.WaitOne();
			queue.Enqueue(val);
			mutex.ReleaseMutex();

			return val;
		}



		public T dequeue()
		{
			mutex.WaitOne();
			T value = (queue.Count == 0) ? default(T) : queue.Dequeue();
			mutex.ReleaseMutex();

			return value;
		}
	}
}
