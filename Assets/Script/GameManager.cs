using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Coin;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadCoin();
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("PlayerCoin", Coin);
        PlayerPrefs.Save();
    }

    public void LoadCoin()
    {
        if (PlayerPrefs.HasKey("PlayerCoin"))
        {
            Coin = PlayerPrefs.GetInt("PlayerCoin");
            if (GameUI.Instance !=null)
            {
                GameUI.Instance.UpdateCoin(Coin);
            }
        }
        else
        {
            Coin = 0;
        }
    }
}
