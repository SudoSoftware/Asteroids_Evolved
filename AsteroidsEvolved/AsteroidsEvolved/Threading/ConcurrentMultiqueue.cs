using System.Collections.Generic;
using System.Threading;

namespace AsteroidsEvolved.Threading
{
	class ConcurrentMultiqueue<T>
	{
		List<T> queue = new List<T>();
		Mutex mutex = new Mutex();


		public T enqueue(T val)
		{
			mutex.WaitOne();
			queue.Add(val);
			mutex.ReleaseMutex();

			return val;
		}



		public bool dequeue(T item)
		{
			mutex.WaitOne();
			bool success = queue.Remove(item);
			mutex.ReleaseMutex();

			return success;
		}
	}
}
