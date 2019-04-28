
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class GeneratorWindow : EditorWindow
{
    public Object prefab, parent;
    public Vector3 min, max;
    public float padding;

    private Stack<List<GameObject>> objectStack;

    [MenuItem("Window/Generator Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GeneratorWindow));
    }

    void OnGUI()
    {
        if (objectStack == null)
            objectStack = new Stack<List<GameObject>>();

        // Prefab line
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Prefab");
        prefab = EditorGUILayout.ObjectField(prefab, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        // Parent object line
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent");
        parent = EditorGUILayout.ObjectField(parent, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        // Min / Max
        min = EditorGUILayout.Vector3Field(new GUIContent("Min"), min);
        max = EditorGUILayout.Vector3Field(new GUIContent("Max"), max);
        padding = EditorGUILayout.FloatField(new GUIContent("Padding"), padding);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate!"))
        {
            if (prefab == null || parent == null)
                ShowNotification(new GUIContent("No object selected for searching"));
            else
            {
                Generate();
            }
        }
        if (GUILayout.Button("Undo"))
        {
            Undo();
        }
        EditorGUILayout.EndHorizontal();
    }

    void Generate()
    {
        var list = new List<GameObject>();
        for (int x = (int)min.x; x <= (int)max.x; x++)
        {
            for (int y = (int)min.y; y <= (int)max.y; y++)
            {
                for (int z = (int)min.z; z <= (int)max.z; z++)
                {
                    GameObject temp = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    temp.transform.SetParent((parent as GameObject).transform);
                    temp.transform.localPosition = new Vector3(x - x * padding, y, z - z * padding);
                    list.Add(temp);
                }
            }
        }
        objectStack.Push(list);
    }

    void Undo()
    {
        if (objectStack.Count > 0)
        {
            var list = objectStack.Pop();
            for (int i = 0; i < list.Count; i++)
            {
                DestroyImmediate(list[i]);
            }
        }
    }
}