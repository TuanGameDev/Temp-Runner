using _Game.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField]
    private GameObject _statHPImg;

    [SerializeField]
    private Transform _container;

    [SerializeField]
    private TextMeshProUGUI _amountCoinTxt;

    [SerializeField]
    private TextMeshProUGUI _timerTxt;

    [SerializeField]
    private TextMeshProUGUI _timerSpeedTxt;

    [SerializeField]
    private TextMeshProUGUI _timerDefTxt;

    [SerializeField]
    private Button _loadGameBtn;

    [SerializeField]
    private GameObject _panelLoseGame;

    [SerializeField]
    private GameObject _iconSpeed;

    [SerializeField]
    private GameObject _iconDef;

    [SerializeField]
    private GameObject _panelSetting;

    [SerializeField]
    private float _timer = 120f;

    public static GameUI Instance;

    private bool _isSetting;

    [SerializeField]
    private PlayerController _playerController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(GetPlayer),1f);
        StartCoroutine(TimerCountdown());
        _loadGameBtn.onClick.AddListener(LoadGame);
    }

    private void GetPlayer()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Setting();
        }
    }

    public void UpdateCoin(int amount)
    {
        _amountCoinTxt.text = amount.ToString("N0");
    }

    public void UpdateHP(int currentHP, int maxHP)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHP; i++)
        {
            Instantiate(_statHPImg, _container);
        }
    }

    private IEnumerator TimerCountdown()
    {
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_timer / 60);
            int seconds = Mathf.FloorToInt(_timer % 60);
            _timerTxt.text = string.Format("{0}:{1:00}", minutes, seconds);
            yield return null;
        }

        _timer = 0;
        Time.timeScale = 0;
        _panelLoseGame.SetActive(true);
        _timerTxt.text = "0:00";
    }

    public void ShowSpeedTimer(float duration)
    {
        _iconSpeed.gameObject.SetActive(true);
        StartCoroutine(SpeedTimerCountdown(duration));
    }

    public void ShowDefTimer(float duration)
    {
        _iconDef.gameObject.SetActive(true);
        StartCoroutine(DefTimerCountdown(duration));
    }

    public void HideSpeedTimer()
    {
        _iconSpeed.gameObject.SetActive(false);
    }

    public void HideDefTimer()
    {
        _iconDef.gameObject.SetActive(false);
    }

    private IEnumerator SpeedTimerCountdown(float duration)
    {
        float timer = duration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(timer);
            _timerSpeedTxt.text = string.Format("X2 Speed: {0}s", seconds);
            yield return null;
        }
        _timerSpeedTxt.text = "Speed: 0s";
    }

    private IEnumerator DefTimerCountdown(float duration)
    {
        float timer = duration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(timer);
            _timerDefTxt.text = string.Format("Armor: {0}s", seconds);
            yield return null;
        }
        _timerDefTxt.text = "Armor: 0s";
    }

    private void Setting()
    {
        _isSetting = !_isSetting;
        _panelSetting.SetActive(_isSetting);

        if (_isSetting)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void LoadGame()
    {
        _panelLoseGame.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("GamePlay");
    }

    public void Continue()
    {
        Time.timeScale = 1;
        _isSetting = false;
        _panelSetting.SetActive(false);
    }

    public void Main()
    {
        SoundManager.Instance.StopSFX(Random.Range(0,1));
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
