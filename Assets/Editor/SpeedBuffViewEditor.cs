using UnityEditor;

[CustomEditor(typeof(SpeedBuffView))]
public class SpeedBuffViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SpeedBuffView speedBuffView = (SpeedBuffView)target;

        DrawDefaultInspector();

        if (speedBuffView.IsTemporary)
        {
            speedBuffView.Duration = EditorGUILayout.FloatField("Duration", speedBuffView.Duration);
        }
    }
}