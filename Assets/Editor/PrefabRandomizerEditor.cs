
using UnityEditor;
using UnityEngine;

public class PrefabRandomizerEditor : EditorWindow
{
    [System.Serializable]
    private class RandomizerSettings
    {
        public GameObject[] prefabPool = new GameObject[0];
        public Vector3 scaleOffset = Vector3.one;
        public Vector3 rotationOffset;
        public Vector3 positionOffset;
        public int numberOfInstances = 10;
        public bool isRandomPosition = false;
    }

    private RandomizerSettings settings = new RandomizerSettings();

    [MenuItem("Tools/Prefab Randomizer")]
    public static void ShowWindow()
    {
        GetWindow<PrefabRandomizerEditor>("Prefab Randomizer");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Prefab List:");
        for (int i = 0; i < settings.prefabPool.Length; i++)
        {
            settings.prefabPool[i] = EditorGUILayout.ObjectField(settings.prefabPool[i], typeof(GameObject), false) as GameObject;
        }

        if (GUILayout.Button("Add Prefab"))
        {
            AddPrefabToArray();
        }

        if (GUILayout.Button("Remove Last Prefab"))
        {
            RemoveLastPrefabFromArray();
        }

        settings.isRandomPosition = EditorGUILayout.Toggle("Is Random Position", settings.isRandomPosition);
        settings.positionOffset = EditorGUILayout.Vector3Field("Position Offset", settings.positionOffset);
        settings.scaleOffset = EditorGUILayout.Vector3Field("Scale Offset", settings.scaleOffset);
        settings.rotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", settings.rotationOffset);
        settings.numberOfInstances = EditorGUILayout.IntField("Number of Instances", settings.numberOfInstances);

        if (GUILayout.Button("Instantiate Prefabs"))
        {
            if (settings.prefabPool.Length > 0)
            {
                foreach (var prefab in settings.prefabPool)
                {
                    if (prefab != null)
                    {
                        // Call the instantiation function from the other script
                        PrefabInstantiator.InstantiatePrefabs(prefab, settings.isRandomPosition, settings.scaleOffset, settings.rotationOffset, settings.positionOffset, settings.numberOfInstances);
                    }
                    else
                    {
                        Debug.LogError("Prefab in the list is not set!");
                    }
                }
            }
            else
            {
                Debug.LogError("Prefab list is empty!");
            }
        }
    }

    private void AddPrefabToArray()
    {
        int length = settings.prefabPool.Length;
        GameObject[] newPrefabs = new GameObject[length + 1];
        for (int i = 0; i < length; i++)
        {
            newPrefabs[i] = settings.prefabPool[i];
        }
        settings.prefabPool = newPrefabs;
    }

    private void RemoveLastPrefabFromArray()
    {
        int length = settings.prefabPool.Length;
        if (length > 0)
        {
            GameObject[] newPrefabs = new GameObject[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                newPrefabs[i] = settings.prefabPool[i];
            }
            settings.prefabPool = newPrefabs;
        }
    }
}
