using System.Threading;

namespace AsteroidsEvolved.Threading
{
	class WorkerThread
	{
		private ConcurrentMultiqueue<WorkItem> workQueue;
		private Thread thread;
		private bool done = false;


		public WorkerThread(ConcurrentMultiqueue<WorkItem> queue)
		{
			workQueue = queue;
			thread = new Thread(new ThreadStart(run));
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
