using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundTitle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _raochanPrefabs;

    [SerializeField]
    private GameObject _coinPrefabs;

    [SerializeField]
    private GameObject[] _itemSO;

    private void Start()
    {
        SpawnRaoChan();
        SpawnCoin();
        SpawnItemSO();
    }

    void SpawnItemSO()
    {
        int randomChance = Random.Range(0, 100);
        if (randomChance < 50)
        {
            int itemIndex = Random.Range(0, _itemSO.Length);
            GameObject tempItem = Instantiate(_itemSO[itemIndex], transform);
            tempItem.transform.position = RanDomPos(GetComponent<Collider>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GroundSpawner.Instance.SpawnTile();
            Destroy(gameObject, 5);
        }
    }

    void SpawnRaoChan()
    {
        int raoChanChiMuc = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(raoChanChiMuc).transform;
        int prefabIndex = Random.Range(0, _raochanPrefabs.Length);
        Instantiate(_raochanPrefabs[prefabIndex], spawnPoint.position, Quaternion.identity, transform);
    }

    void SpawnCoin()
    {
        GameObject temp = Instantiate(_coinPrefabs, transform);
        temp.transform.position = RanDomPos(GetComponent<Collider>());
    }

    Vector3 RanDomPos(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        if (point != collider.ClosestPoint(point))
        {
            point = RanDomPos(collider);
        }
        point.y = 1;
        return point;
    }
}
