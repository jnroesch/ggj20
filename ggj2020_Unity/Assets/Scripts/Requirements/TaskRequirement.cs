using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskRequirement
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
}
