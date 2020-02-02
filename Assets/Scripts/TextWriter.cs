using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private Text uiText;
    private string textToWrite;
    private int charIndex;
    private float timePerChar;
    private float timer;
    private bool invisivleChar;

    public void AddWriter(Text uiText, string textToWrite, float timePerChar, bool invisivleChar)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite.Replace("\\n", "\n");
        this.timePerChar = timePerChar;
        this.invisivleChar = invisivleChar;
        charIndex = 0;
    }
    private void Update()
    {
        if (uiText != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerChar;
                charIndex++;
                string text = textToWrite.Substring(0, charIndex);

                if (invisivleChar)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(charIndex) + "</color>";
                }
                uiText.text = text;

                if (charIndex >= textToWrite.Length)
                {
                    uiText = null;
                    return;
                }
            }
        }
    }
}
