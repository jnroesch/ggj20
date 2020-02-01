using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.Abstract
{
	public abstract class GameTask
	{
		public bool Completed
		{
			get
			{
				if (!_completed)
				{
					_completed = IsCompleted();
				}

				return _completed;
			}
		}

		private bool _completed;

		public abstract bool IsCompleted();

		public abstract void StartTask();

		public abstract void WinTask();

		public abstract void FailTask();
	}
}