﻿using System.Threading;

namespace AsteroidsEvolved.Threading
{
	abstract class WorkItem
	{
		public abstract void execute();

		public void complete()
		{
			Thread.Sleep(10); //sleep to keep at a sane update rate
		}
	}
}
