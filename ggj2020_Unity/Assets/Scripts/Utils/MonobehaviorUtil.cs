using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils
{
	public class MonobehaviorUtil : MonoBehaviour
	{
		public static MonobehaviorUtil Instance;

		private void Awake()
		{
			Instance = this;
		}

		public string GetPersistentDataPath()
		{
			return Application.persistentDataPath;
		}

		public string GetDataPath()
		{
			return Application.dataPath;
		}

		public void Write(string text)
		{
			Debug.Log(text);
		}
	}
}