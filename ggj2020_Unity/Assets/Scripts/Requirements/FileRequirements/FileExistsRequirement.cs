using Game.Utils;
using System.Collections.Generic;
using System.IO;

namespace Game.Requirements.FileRequirements
{
	public class FileExistsRequirement : TaskRequirement
	{
		//TODO: put this somewhere else, either in a file or in a constants class
		private List<string> options = new List<string>
		{
			"hallo.txt",
			"test.png",
			"stuff.pdf"
		};

		private string _fileName;

		public FileExistsRequirement()
		{
			var random = new System.Random();
			var index = random.Next(options.Count);

			_fileName = options[index];

			MonobehaviorUtil.Instance.Write("file required with filename "+_fileName);
		}

		public override bool IsCompleted()
		{
			var path = MonobehaviorUtil.Instance.GetPersistentDataPath();
			var fullPath = Path.Combine(path, _fileName);
			return File.Exists(fullPath);
		}
	}
}