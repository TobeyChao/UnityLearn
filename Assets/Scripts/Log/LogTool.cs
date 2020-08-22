using System;
using System.Collections.Generic;
using UnityEngine;

public class LogTool : MonoBehaviour
{
    private readonly List<string> m_Logs = new List<string>();
    private bool m_IsVisible = false;
    private Rect m_LogWindow = new Rect(0, 0, 400, 400);
    private Vector2 m_ScrollPosText;


    // Start is called before the first frame update
    void Start()
    {
        Application.logMessageReceived += Application_logMessageReceived;
        for (int i = 0; i < 10; i++)
        {
            Debug.LogError("TobeyChao");
        }
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            if (!m_IsVisible)
            {
                m_IsVisible = true;
            }
            m_Logs.Add(string.Format("{0}\n{1}", condition, stackTrace));
        }
    }

    private void OnGUI()
    {
        if (m_IsVisible)
        {
            m_LogWindow = GUILayout.Window(0, m_LogWindow, ConsoleWindow, "Console");
        }
    }

    private void ConsoleWindow(int id)
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear", GUILayout.MaxWidth(200)))
        {
            m_Logs.Clear();
        }

        if (GUILayout.Button("Close", GUILayout.MaxWidth(200)))
        {
            m_IsVisible = false;
        }
        GUILayout.EndHorizontal();
        m_ScrollPosText = GUILayout.BeginScrollView(m_ScrollPosText);

        foreach (var log in m_Logs)
        {
            Color color = GUI.contentColor;
            GUI.contentColor = Color.red;
            GUILayout.TextArea(log);
            GUI.contentColor = color;
        }

        GUILayout.EndScrollView();
    }
}
