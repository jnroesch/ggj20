using Game;
using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Tasks.FileTasks
{
	public class FileExistsTask : FileTask
	{
		private List<string> options = new List<string>()
	{
		"sysconfig.txt",
		"manual.pdf",
		"report.txt",
		"system.txt",
		"users.txt",
	};

		private string _fileName;

		public override void FailTask()
		{
			throw new System.NotImplementedException();
		}

		public override bool IsCompleted()
		{
			var path = Path.Combine(Application.persistentDataPath, _fileName);
			return File.Exists(path);
		}

		public override void StartTask()
		{
			var random = new System.Random();
			int index = random.Next(options.Count);
			_fileName = options[index];

			var path = Path.Combine(Application.dataPath, _fileName);

			//if file already exists, remove it so task does not get auto completed
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			GameManager.Instance.LogToConsole("One file is missing in filesystem: " + _fileName);
		}

		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());

			GameManager.Instance.LogToConsole("File has been restored - Good Job!");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}