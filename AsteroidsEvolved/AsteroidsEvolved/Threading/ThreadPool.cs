using System.Threading;
using System.Collections.Generic;

namespace AsteroidsEvolved.Threading
{
	class ThreadPool
	{
		private static ThreadPool instance;
		private Queue<WorkerThread> threadQueue;
		private ConcurrentMultiqueue<WorkItem> workQueue;


		protected ThreadPool(int numberOfThreads)
		{
			for (int j = 0; j < numberOfThreads + 4; j++)
				threadQueue.Enqueue(new WorkerThread(workQueue));
		}



		public WorkItem enqueueWorkItem(WorkItem item)
		{
			return workQueue.enqueue(item);
		}



		public static void terminate()
		{
			if (instance == null)
				return;

			//TODO
		}



		public ThreadPool getInstance()
		{
			if (instance != null)
				return instance;

			int availableThreads = System.Environment.ProcessorCount;
			instance = new ThreadPool(availableThreads);
			return instance;
		}



	}
}
