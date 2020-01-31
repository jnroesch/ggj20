using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    void Start()
    {
		var requirement = new FileExistsRequirement("hallo.txt");
		var requirements = new List<TaskRequirement>()
		{
			requirement
		};
		var task = new FileTask(requirements);

		Debug.Log(task.Completed);

    }
}
