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
			var path = Path.Combine(Application.dataPath, _fileName);
			return !File.Exists(path);
		}

		public override void StartTask()
		{
			File.Create(Path.Combine(Application.dataPath, _fileName));

			GameManager.Instance.LogToConsole(@"
A virus created a malicious file in [local] files: " + _fileName + @" please remove it.


	REMOVE FILE.


");
		}

		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());

			GameManager.Instance.LogToConsole("Quarantined.");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}