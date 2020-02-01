using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Console.Commands
{
	public class ConsoleCommand : MonoBehaviour
	{
		public string Command;

		public UnityEvent action;

		public void OpenPersistentDataPath()
		{
			var path = Application.persistentDataPath + Path.DirectorySeparatorChar;
			path.Replace("/", "\\"); // windows explorer doesn't like forward slashes
			UnityEngine.Debug.Log(path);
			
			Process.Start("explorer.exe", "/open,"+ path);
		}

		public void OpenDataPath()
		{
			var path = "@" + Application.dataPath;
			Process.Start("explorer.exe", path);
		}

		public void Help()
		{
			MonobehaviorUtil.Instance.Write("looking for help? too bad");
		}
	}
}