using UnityEngine;
using System.Collections;

public class CardSpawner : MonoBehaviour
{
    private GameObject[] spawnPoints;
    private bool Run
    {
        get
        {
            return true;
        }
    }

    void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("CardSpawn"); //Finds the spawnpoints for the cards
        Spawn(spawnPoints, Resources.Load("Prefabs/Card") as GameObject); //spawn Cards
    }

    void Spawn(GameObject[] spawnPoints, GameObject prefab)
    {
        for (int i = 0; i < spawnPoints.Length; ++i)
        {
                GameObject go = Instantiate(prefab);
                go.transform.position = spawnPoints[i].transform.position;
                spawnPoints[i] = null;
        }

    }
}
