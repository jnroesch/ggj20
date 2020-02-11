using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Tasks.Abstract;
using System.IO;
using Game;
using Game.Console;
using System;

public class EditFileTask : FileTask
{
    private string _fileName = "meta.sys";
    private string _path;
    private int _lineIndex;
    private string _targetString;
    private bool _isCompleted;
    private FileSystemWatcher _watcher = new FileSystemWatcher();

    public override void FailTask()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsCompleted()
    {
        return _isCompleted;
    }

    public override void StartTask()
    {
        int linesAmout = 5;

        var random = new System.Random();
        _lineIndex = random.Next(0, linesAmout);
        _targetString = Convert.ToString(random.Next(0, 32), 2).PadLeft(5, '0');

        _path = Path.Combine(Application.persistentDataPath, _fileName);

        StreamWriter writer = new StreamWriter(_path, false);

        for (int i = 0; i < linesAmout;)
        {
            string line = Convert.ToString(random.Next(0, 32), 2).PadLeft(5, '0');
            if (i != _lineIndex || line != _targetString)
            {
                writer.WriteLine(line);
                i++;
            }
        }

        writer.Close();

        _watcher.Path = Application.persistentDataPath;
        _watcher.Changed += OnFilesystemChange;
        _watcher.EnableRaisingEvents = true;


        GameConsole.instance.Log($@"
Machine code requires adjustments. Change line {_lineIndex + 1} in [external] file {_fileName} to {_targetString};

	EDIT FILE.
");
    }

    public void OnFilesystemChange(object source, FileSystemEventArgs e)
    {
        Debug.Log("change");

        string[] lines = File.ReadAllLines(_path);
        if (lines[_lineIndex] == _targetString)
        {
            _isCompleted = true;
        }
        else
        {
            _isCompleted = false;
        }
    }

    public override void WinTask()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());

        _watcher.Changed -= OnFilesystemChange;

        GameConsole.instance.Log("Machine behaviour adjusted.");
        GameManager.Instance.FinishCurrentTask();
    }
}
