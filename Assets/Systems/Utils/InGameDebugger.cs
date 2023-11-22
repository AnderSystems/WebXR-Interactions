using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class InGameDebugger : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        Application.logMessageReceivedThreaded += LogMessageUpdate;
    }
    private void LogMessageUpdate(string condition, string stackTrace, LogType type)
    {
        string message = $"</b>{condition}<b>\n{stackTrace}";
        string txt = "";
        switch (type)
        {
            case LogType.Error:
                txt = $"<b><color=red>[ERROR!]</b> {message} </color>";
                break;
            case LogType.Assert:
                txt = $"<b><color=white>[Assert]</b> {message} </color>";
                break;
            case LogType.Warning:
                txt = $"<b><color=orange>[Warning!]</b> {message} </color>";
                break;
            case LogType.Log:
                txt = $"<b><color=white>[Log]</b> {message} </color>";
                break;
            case LogType.Exception:
                txt = $"<b><color=red>[Exception]</b> {message} </color>";
                break;
            default:
                break;
        }


        text.text += "\n" + txt;
    }

    private void OnValidate()
    {
        if (!text)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
    }
}
