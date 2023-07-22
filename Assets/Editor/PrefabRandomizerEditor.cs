using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabRandomizerEditor : EditorWindow
{
    private SerializedObject serializedPrefabRandomizer;
    private SerializedProperty prefabPoolProperty;
    private int numberOfInstances = 10;
    private Vector3 positionOffset = Vector3.zero;
    private Vector3 rotationOffset = Vector3.zero;
    private Vector3 scaleOffset = Vector3.one;

    [MenuItem("Tools/Prefab Randomizer")]
    public static void ShowWindow()
    {
        GetWindow<PrefabRandomizerEditor>("Prefab Randomizer");
    }

    private void OnEnable()
    {
        // Create a SerializedObject for the PrefabRandomizer script
        serializedPrefabRandomizer = new SerializedObject(PrefabRandomizer.Instance);
        prefabPoolProperty = serializedPrefabRandomizer.FindProperty("prefabPool");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Prefab Randomizer Tool", EditorStyles.boldLabel);

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Prefab Pool", EditorStyles.miniBoldLabel);
        EditorGUILayout.PropertyField(prefabPoolProperty, true);

        serializedPrefabRandomizer.ApplyModifiedProperties();

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Randomization Settings", EditorStyles.miniBoldLabel);
        numberOfInstances = EditorGUILayout.IntSlider("Number of Instances", numberOfInstances, 1, 100);
        positionOffset = EditorGUILayout.Vector3Field("Position Offset", positionOffset);
        rotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", rotationOffset);
        scaleOffset = EditorGUILayout.Vector3Field("Scale Offset", scaleOffset);

        EditorGUILayout.Space(20);

        if (GUILayout.Button("Randomize Prefabs"))
        {
            RandomizePrefabs();
        }
    }

    private void RandomizePrefabs()
    {
        PrefabRandomizer.Instance.Randomize(numberOfInstances, positionOffset, rotationOffset, scaleOffset);
    }
}
