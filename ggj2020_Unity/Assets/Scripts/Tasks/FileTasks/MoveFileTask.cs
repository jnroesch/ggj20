using Game.Tasks.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Game.Tasks.FileTasks
{
	public class MoveFileTask : FileTask
	{
		private string _fileName;

		private List<string> options = new List<string>()
		{
			"referencetable.xml",
			"lifesupport.dll",
			"nsfhuman.gif",
		};

		public override void FailTask()
		{
			throw new System.NotImplementedException();
		}

		public override bool IsCompleted()
		{
			var oldPath = Path.Combine(Application.persistentDataPath, _fileName);
			var newPath = Path.Combine(Application.dataPath, _fileName);

			return File.Exists(newPath) && !File.Exists(oldPath);
		}

		public override void StartTask()
		{
			var files = Directory.GetFiles(Application.persistentDataPath).Select(file => file.Split(new char[] { '/', '\\' }).Last()).ToList();

			files.Remove("Player.log");
			files.Remove("Player-prev.log");

			if (files.Count == 0)
			{
				var random = new System.Random();
				int index = random.Next(options.Count);
				_fileName = options[index];

				File.Create(Path.Combine(Application.persistentDataPath, _fileName));
			}
			else
			{
				var random = new System.Random();
				var index = random.Next(files.Count);
				_fileName = files[index].Split(new char[] { '/', '\\' }).Last();
			}

			GameManager.Instance.LogToConsole($@"
File has been placed in wrong directory. Please move file from [external] to [local]: {_fileName}

	MOVE FILE.
");
		}

		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());

			GameManager.Instance.LogToConsole("Integrity restored.");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}