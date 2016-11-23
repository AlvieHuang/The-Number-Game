using UnityEngine;
using System.Collections;

public class CardSpawner : MonoBehaviour
{
    public int SpawnCount;
    public int SpawnSpots;
    private int SpawnCounter;
    private GameObject[] spawnPoints;
    private bool Run
    {
        get
        {
            return true;
        }
    }

    public static void RemoveAt(ref GameObject[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
    }

    void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("CardSpawn"); //Finds the spawnpoints for the cards
        Spawn(spawnPoints, Resources.Load("Prefabs/Card") as GameObject); //spawn Cards
    }

    void Spawn(GameObject[] spawnPoints, GameObject prefab)
    {
        SpawnCounter = 0;
        while (SpawnCounter < SpawnCount)
        {
            int x = Random.Range(0, SpawnSpots);
            GameObject go = Instantiate(prefab);
            go.transform.position = spawnPoints[x].transform.position;
            spawnPoints[x] = null;
            RemoveAt(ref spawnPoints, x);
            SpawnCounter++;
            SpawnSpots--;
        }
    }
}
