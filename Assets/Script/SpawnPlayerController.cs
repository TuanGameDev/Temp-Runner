using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerController : MonoBehaviour
{
    [SerializeField]

    private GameObject _playerPrefabs;

    private void Start()
    {
        SpawnPlayer();
    }
    void SpawnPlayer()
    {
         Instantiate(_playerPrefabs,transform.position, Quaternion.identity);
    }
}
