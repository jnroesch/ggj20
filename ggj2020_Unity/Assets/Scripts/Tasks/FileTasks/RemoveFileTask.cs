using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Tasks.FileTasks
{
	public class RemoveFileTask : FileTask
	{
		private string _fileName = "virus.txt";

		public override void FailTask()
		{
			throw new System.NotImplementedException();
		}

		public override bool IsCompleted()
		{
			var path = Path.Combine(Application.persistentDataPath, _fileName);
			return !File.Exists(path);
		}

		public override void StartTask()
		{
			File.Create(Path.Combine(Application.persistentDataPath, _fileName));

			GameManager.Instance.LogToConsole("The virus created a malicious file: " + _fileName);
		}

		public override void WinTask()
		{
			GameManager.Instance.LogToConsole("well done");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}