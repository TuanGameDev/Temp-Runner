using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public static GroundSpawner Instance;

    private Vector3 _nextSpawnPoint;

    public GameObject GroundTile;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int  i =0;i<20;i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject temp = Instantiate(GroundTile, _nextSpawnPoint, Quaternion.identity);
        _nextSpawnPoint = temp.transform.GetChild(1).position;
    }
}
