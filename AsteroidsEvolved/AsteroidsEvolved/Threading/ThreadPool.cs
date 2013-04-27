using System.Threading;
using System.Collections.Generic;

namespace AsteroidsEvolved.Threading
{
	class ThreadPool
	{
		private static ThreadPool instance;
		private Queue<WorkerThread> threadQueue = new Queue<WorkerThread>();
		private ConcurrentMultiqueue<WorkItem> workQueue = new ConcurrentMultiqueue<WorkItem>();


		protected ThreadPool(int numberOfThreads)
		{
			for (int j = 0; j < numberOfThreads; j++)
				threadQueue.Enqueue(new WorkerThread(workQueue));
		}



		public void enqueueWorkItem(WorkItem item)
		{
			workQueue.enqueue(item);
		}



        public WorkItem dequeueWorkItem(WorkItem item)
        {
			return workQueue.dequeue();
        }



		public void startWork()
		{
			foreach (WorkerThread wt in threadQueue)
				wt.start();
		}



		public void terminate()
		{
			if (instance == null)
				return;

			List<WorkerThread> threads = new List<WorkerThread>(threadQueue.Count);
			while (threadQueue.Count > 0)
			{
				WorkerThread wt = threadQueue.Dequeue();
				wt.terminate();
				threads.Add(wt);
			}

			foreach (WorkerThread tw in threads)
				tw.join();

			instance = null;
		}



		public static ThreadPool getInstance()
		{
			if (instance != null)
				return instance;

			int availableThreads = System.Environment.ProcessorCount;
			instance = new ThreadPool(availableThreads);
			return instance;
		}
	}
}
