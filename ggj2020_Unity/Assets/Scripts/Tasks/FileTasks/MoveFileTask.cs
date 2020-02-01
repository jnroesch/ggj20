﻿using Game.Tasks.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Tasks.FileTasks
{
	public class MoveFileTask : FileTask
	{
		private string _fileName;

		public MoveFileTask()
		{
			var files = Directory.GetFiles(Application.persistentDataPath);

			var random = new System.Random();
			var index = random.Next(files.Length);
			_fileName = files[index];
		}

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
			GameManager.Instance.LogToConsole($"File has been placed in wrong directory<br>Conflicitng file is {_fileName}");
		}

		public override void WinTask()
		{
			GameManager.Instance.LogToConsole("files have been correctly moved");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}