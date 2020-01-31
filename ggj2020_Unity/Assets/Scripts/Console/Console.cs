using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public GameObject logEntryPrefab;
    public Transform logEntryParent;
    public InputField inputField;

    public List<Text> logEntries = new List<Text>();

    private void Awake()
    {
        inputField = GetComponent<InputField>();
        inputField.text = ">";
    }

    private void OnApplicationFocus(bool focus)
    {
        inputField.ActivateInputField();
    }

    public void CheckForLineEnd(string currentInputText)
    {
        if (currentInputText.EndsWith("\n"))
        {
            CreateLogEntry(currentInputText);
        }
    }

    public void CreateLogEntry(string logEntryText)
    {
        Text logEntry = Instantiate(logEntryPrefab, logEntryParent).GetComponent<Text>();
        logEntry.text = logEntryText;

        logEntries.Add(logEntry);

        inputField.text = ">";
        //inputField.ActivateInputField();
    }
}
