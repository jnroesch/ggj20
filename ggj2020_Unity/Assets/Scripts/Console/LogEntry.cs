using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Console
{
    public class LogEntry : MonoBehaviour
    {
        public TMP_InputField inputField;

        public void SetText(string text)
        {
            inputField.text = text;
        }

        public string GetText()
        {
            return inputField.text;
        }
    }
}
