using System.Threading;

namespace AsteroidsEvolved.Threading
{
	class WorkerThread
	{
		private static int instanceCount = 0;
		private ConcurrentMultiqueue<WorkItem> workQueue;
		private Thread thread;
		private bool done = false;


		public WorkerThread(ConcurrentMultiqueue<WorkItem> queue)
		{
			workQueue = queue;
			thread = new Thread(new ThreadStart(run));
			thread.Name = "" + (instanceCount++);
		}



		public void run()
		{
			while (!done)
			{
				WorkItem item = workQueue.dequeue();
				if (item != null)
				{
					item.execute();
					item.complete();
					workQueue.enqueue(item);
				}
			}
		}



		public void start()
		{
			thread.Start();
		}



		public void terminate()
		{
			done = true;
		}



		public void join()
		{
			thread.Join();
		}
	}
}
