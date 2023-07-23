using UnityEngine;

public static class PrefabInstantiator
{
    public static void InstantiatePrefabs(GameObject prefab, bool isRandomPosition, Vector3 scaleOffset, Vector3 rotationOffset, Vector3 positionOffset, int numberOfInstances)
    {
        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 randomPosition = Vector3.zero;
            if (isRandomPosition)
            {
                randomPosition = new Vector3(positionOffset.x + Random.Range(-20f, 20f), positionOffset.y, positionOffset.z + Random.Range(-30f, 30f));
            }

            Quaternion randomRotation = Quaternion.Euler(Random.Range(-rotationOffset.x, rotationOffset.x),
                                                         Random.Range(-rotationOffset.y, rotationOffset.y),
                                                         Random.Range(-rotationOffset.z, rotationOffset.z));
            GameObject instance = Object.Instantiate(prefab, randomPosition + positionOffset, randomRotation);
            instance.transform.localScale += scaleOffset;
        }
    }
}