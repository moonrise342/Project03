using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabRandomizerEditor : EditorWindow
{
    private GameObject[] prefabPool;
    private int numberOfInstances = 10;
    private Vector3 positionOffset = Vector3.zero;
    private Vector3 rotationOffset = Vector3.zero;
    private Vector3 scaleOffset = Vector3.one;

    [MenuItem("Tools/Prefab Randomizer")]
    public static void ShowWindow()
    {
        GetWindow<PrefabRandomizerEditor>("Prefab Randomizer");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Prefab Randomizer Tool", EditorStyles.boldLabel);

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Prefab Pool", EditorStyles.miniBoldLabel);
        prefabPool = EditorGUILayout.ObjectField("Prefab Pool", prefabPool, typeof(GameObject[]), true) as GameObject[];

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
        if (prefabPool == null || prefabPool.Length == 0)
        {
            Debug.LogWarning("Prefab Pool is empty. Add prefabs to the pool.");
            return;
        }

        for (int i = 0; i < numberOfInstances; i++)
        {
            GameObject prefabToInstantiate = prefabPool[Random.Range(0, prefabPool.Length)];
            Vector3 randomPosition = Random.insideUnitSphere * 5f + positionOffset;
            Vector3 randomRotation = rotationOffset;
            Vector3 randomScale = new Vector3(Random.Range(0.5f, 1.5f) * scaleOffset.x,
                                              Random.Range(0.5f, 1.5f) * scaleOffset.y,
                                              Random.Range(0.5f, 1.5f) * scaleOffset.z);

            GameObject instance = PrefabUtility.InstantiatePrefab(prefabToInstantiate) as GameObject;
            instance.transform.position = randomPosition;
            instance.transform.rotation = Quaternion.Euler(randomRotation);
            instance.transform.localScale = randomScale;
        }
    }
}
