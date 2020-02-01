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
        public TMP_Text directoryText;

        private string[] lines;
        private float delay = .03f;

        public void SetText(string text)
        {
            inputField.text = text;
        }

        public string GetText()
        {
            return inputField.text;
        }

        public void SetDirectoryText(string text)
        {
            directoryText.text = text;
        }

        public string GetDirectoryText()
        {
            return directoryText.text;
        }

        public void SetTextGameBoy(string text)
        {
            StartCoroutine(GameBoyText(text));
        }

        private IEnumerator GameBoyText(string text)
        {
            yield return new WaitForSeconds(delay);

            for (int i = 0; i < text.Length; i++)
            {
                inputField.text += text[i];
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
