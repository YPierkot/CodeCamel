using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class StaticEditor{
    public static GUIStyle buttonStyle = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 12 };
    public static GUIStyle labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 12 };

    /// <summary>
    /// Create a progressBar
    /// </summary>
    /// <param name="value"></param>
    /// <param name="label"></param>
    public static void ProgressBar(float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        Space(2);
    }

    /// <summary>
    /// Make a space in the editor
    /// </summary>
    /// <param name="value"></param>
    public static void Space(int value)
    {
        GUILayout.Space(value);
    }
}
