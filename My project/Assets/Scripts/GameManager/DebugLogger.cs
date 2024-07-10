using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Collections.Generic; // Import the namespace for List

public class DebugLogger : MonoBehaviour
{
    public TextMeshProUGUI debugText; // Reference to the TextMeshProUGUI component
    private List<string> logMessages = new List<string>(); // List to store log messages
    private const int maxMessages = 10; // Maximum number of messages to display

    void OnEnable()
    {
        Debug.Log("DebugLogger enabled.");
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Debug.Log("DebugLogger disabled.");
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Add the new log message to the list
        logMessages.Add(logString);

        // Ensure we only keep the last maxMessages messages
        if (logMessages.Count > maxMessages)
        {
            logMessages.RemoveAt(0); // Remove the oldest message
        }

        // Combine the messages into a single string and update the TextMeshProUGUI component
        debugText.text = string.Join("\n", logMessages.ToArray());

        Debug.Log("HandleLog called with message: " + logString);
        if (debugText != null)
        {
            Debug.Log("Debug text updated.");
        }
        else
        {
            Debug.LogError("Debug text is not assigned!");
        }
    }
}