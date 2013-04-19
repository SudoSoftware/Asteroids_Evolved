
namespace AsteroidsEvolved.Threading
{
	abstract class WorkItem
	{
		public abstract void execute();

		public void complete() //todo: is this necessary?
		{ }
	}
}
