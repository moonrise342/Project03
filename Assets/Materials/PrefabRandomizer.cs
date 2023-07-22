using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabRandomizer", menuName = "ScriptableObjects/PrefabRandomizer", order = 1)]
public class PrefabRandomizer : ScriptableObject
{
    private static PrefabRandomizer instance;
    public static PrefabRandomizer Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<PrefabRandomizer>("PrefabRandomizer");

            return instance;
        }
    }

    public GameObject[] prefabPool;

    public void Randomize(int numberOfInstances, Vector3 positionOffset, Vector3 rotationOffset, Vector3 scaleOffset)
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

            GameObject instance = Instantiate(prefabToInstantiate, randomPosition, Quaternion.Euler(randomRotation));
            instance.transform.localScale = randomScale;
        }
    }
}

