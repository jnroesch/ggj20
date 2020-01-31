using System.IO;

public class FileExistsRequirement : TaskRequirement
{
	private string _fileName;

	public FileExistsRequirement(string fileName)
	{
		_fileName = fileName;
	}

	public override bool IsCompleted()
	{
		var path = MonobehaviorUtil.Instance.GetPersistentDataPath();
		var fullPath = Path.Combine(path, _fileName);
		MonobehaviorUtil.Instance.Write(fullPath);
		return File.Exists(fullPath);
	}
}
