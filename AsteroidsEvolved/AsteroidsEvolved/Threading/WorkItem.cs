

namespace AsteroidsEvolved.Threading
{
	abstract class WorkItem
	{
		/*public enum Priority
		{
			One = 1,
			Two = 2,
			Three = 3
		};

		protected Priority priority = Priority.One;*/


		public abstract void execute();
		public abstract void complete();
		

		/*
		public Priority getPriority()
		{
			return priority;
		}



		public void setPriority(Priority newPriority)
		{
			priority = newPriority;
		}*/
	}
}
