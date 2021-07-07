using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class StaticEditor{
    #region Variables
    public static GUIStyle buttonTitleStyle = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 14 };
    public static GUIStyle buttonStyle = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 12 };

    public static GUIStyle labelTitleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 14 };
    public static GUIStyle labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12 };
    #endregion Variables

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
    public static void Space(int value = 8){
        GUILayout.Space(value);
    }

    #region Box
    /// <summary>
    /// Begin a verticalLayout with a box
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void VerticalBox(int width = -1, int height = -1){
        GUILayout.BeginVertical("box", 
            width == -1? GUILayout.ExpandWidth(true) : GUILayout.Width(width), 
            height == -1 ? GUILayout.ExpandHeight(true) : GUILayout.Height(width));
    }

    /// <summary>
    /// Begin a horizontalLayout with a box
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void HorizontalBox(int width = -1, int height = -1){
        GUILayout.BeginHorizontal("box",
            width == -1 ? GUILayout.ExpandWidth(true) : GUILayout.Width(width),
            height == -1 ? GUILayout.ExpandHeight(true) : GUILayout.Height(width));
    }
    #endregion Box
}
