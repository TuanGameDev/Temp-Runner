using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameUserTxt;

    [SerializeField]
    private TMP_InputField _nameInputField;

    [SerializeField]
    private TextMeshProUGUI _amountCoinTxt;

    [SerializeField]
    private Button _startGameBtn;

    [SerializeField]
    private Button _selectedNameBtn;

    [SerializeField]
    private GameObject _panelNamePopup;

    private const string UserNameKey = "UserName";

    private void Start()
    {
        _startGameBtn.onClick.AddListener(StartGame);
        _selectedNameBtn.onClick.AddListener(SetUserName);

        CheckUserName();
        UpdateCoin();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateCoin()
    {
        _amountCoinTxt.text = GameManager.Instance.Coin.ToString("N0");
    }

    private void CheckUserName()
    {
        if (PlayerPrefs.HasKey(UserNameKey))
        {
            string userName = PlayerPrefs.GetString(UserNameKey);
            _nameUserTxt.text = userName;
            _panelNamePopup.SetActive(false);
        }
        else
        {
            _panelNamePopup.SetActive(true);
        }
    }

    private void SetUserName()
    {
        string inputName = _nameInputField.text;

        if (!string.IsNullOrEmpty(inputName))
        {
            PlayerPrefs.SetString(UserNameKey, inputName);
            PlayerPrefs.Save();

            _nameUserTxt.text = inputName;
            _panelNamePopup.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Name input is empty. Please enter a valid name.");
        }
    }
}
