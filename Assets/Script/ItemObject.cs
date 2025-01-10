using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    private ItemSO _itemSO;
    [SerializeField]
    private float _rotationSpeed = 80f;
    private void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                if (_itemSO.BonusHP > 0)
                {
                    player.IncreaseHP(_itemSO.BonusHP);
                }

                if (_itemSO.BonusSpeed > 0)
                {
                    player.ActivateSpeedBonus(_itemSO.BonusSpeed, 30f);
                }

                if (_itemSO.BonusDef > 0)
                {
                    player.ActivateDefBonus(_itemSO.BonusDef, 15f);
                }
            }

            Destroy(gameObject);
        }
    }
}
