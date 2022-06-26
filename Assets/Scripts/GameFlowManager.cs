using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;
    public Picker _picker;
    public int currentLevel = 1;
    public GameStates gameState = GameStates.Stop;
    public MusicStates musicState = MusicStates.Playing;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        _picker = GameObject.Find("Picker").GetComponent<Picker>();
        _picker.enabled = false;
    }


    public void CreateLevel(bool isNextLevel)
    {
        UIManager.Instance.popup.SetActive(false);
        UIManager.Instance._gameScreen.SetActive(true);
        _picker.transform.position = Vector3.zero;
        Destroy(LevelGenerator.Instance.myLevel.gameObject);
        LevelGenerator.Instance.CreateLevel(isNextLevel? ++currentLevel : currentLevel);
        PlayerPrefs.SetInt("currentLevel",currentLevel);
        UIManager.Instance.SetDragToStartActive(true);
        gameState = GameStates.Stop;
    }

    public void ToggleMusicState()
    {
        bool check = musicState == MusicStates.Playing;
        UIManager.Instance.soundPassive.SetActive(check);
        UIManager.Instance.soundActive.SetActive(!check);
        AudioListener.volume = check ? 0f : 1f;
        musicState = check ? MusicStates.Mute : MusicStates.Playing;
    }

}

public enum GameStates
{
    Running, Stop
}

public enum MusicStates
{
    Playing, Mute
}
