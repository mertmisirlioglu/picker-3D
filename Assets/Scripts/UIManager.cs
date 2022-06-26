using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager Instance;

    [Header("AllScreens")]
    [SerializeField] private GameObject _startScreen;
    public GameObject _gameScreen;
    [SerializeField] private GameObject _endScreen;

    [Header("StartScreen")]
    [SerializeField] private GameObject _logoStart;
    [SerializeField] private GameObject _titleTextStart;
    [SerializeField] private GameObject _buttonStart;

    [Header("InGameCanvas")]
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public Image progressBarFillImage;
    public GameObject dragToStartObject;
    public GameObject soundActive;
    public GameObject soundPassive;

    [Header("EndGameCanvas")]
    public GameObject popup;
    public GameObject winText;
    public GameObject loseText;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartScreenAnimation();
    }

    void StartScreenAnimation()
    {
        _logoStart.transform.DOScale(new Vector2(1, 1), 1f).OnComplete(() =>
        {
            _buttonStart.GetComponent<RectTransform>().DOAnchorPosX(0, 0.5f);
            _titleTextStart.GetComponent<RectTransform>().DOAnchorPosX(0, 0.5f);
        });

        _logoStart.transform.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetRelative(true)
            .SetEase(Ease.Linear);
    }

    public void PlayButtonClicked()
    {
        _startScreen.SetActive(false);
        _gameScreen.SetActive(true);

        // add player prefs and load level
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        currentLevel = currentLevel != 0 ? currentLevel : 1;
        LevelGenerator.Instance.CreateLevel(currentLevel);

        currentLevelText.SetText(currentLevel.ToString());
        nextLevelText.SetText((currentLevel + 1).ToString());
        GameFlowManager.Instance.currentLevel = currentLevel;
        GameFlowManager.Instance._picker.enabled = true;
    }

    public void SetWin()
    {
        _gameScreen.SetActive(false);
        popup.SetActive(true);
        loseText.SetActive(false);
        winText.SetActive(true);
    }

    public void SetLose()
    {
        _gameScreen.SetActive(false);
        winText.SetActive(false);
        popup.SetActive(true);
        loseText.SetActive(true);
    }

    public void SetCurrentLevelsInGameScreen()
    {
        int currentLevel = GameFlowManager.Instance.currentLevel;
        currentLevelText.SetText(currentLevel.ToString());
        nextLevelText.SetText((currentLevel+1).ToString());
    }

    public void SetDragToStartActive(bool isActive)
    {
        dragToStartObject.SetActive(isActive);
    }
}
