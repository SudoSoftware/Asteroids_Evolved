

namespace AsteroidsEvolved.Threading
{
	abstract class WorkItem
	{
		public abstract void execute();
		public abstract void complete();
	}
}
