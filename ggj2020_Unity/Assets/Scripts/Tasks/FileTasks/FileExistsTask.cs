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
		"hallo.txt",
		"test.png",
		"stuff.pdf"
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

			GameManager.Instance.LogToConsole("One file is missing in filesystem: " + _fileName);
		}

		public override void WinTask()
		{
			GameManager.Instance.LogToConsole("File has been restored - Good Job!");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}