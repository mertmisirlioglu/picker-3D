using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    [Header("Prefabs")]
    public GameObject level;
    public GameObject[] ballGroups;

    public Level myLevel;

    private int[] maxBallsInGrounds;
    private int slotCounter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreateLevel(int levelNumber)
    {
        GameObject levelGameObject = Instantiate(level);
        myLevel = levelGameObject.GetComponent<Level>();
        maxBallsInGrounds = new int[myLevel.ballCounters.Length];
        slotCounter = 0;
        CreateBalls();
        CreateTargets(levelNumber);
        SetPickerMoveSpeed(levelNumber);
        UIManager.Instance.SetCurrentLevelsInGameScreen();
    }

    void CreateBalls()
    {
        foreach (var ballSlot in myLevel.ballSlots)
        {
            int random = Random.Range(0, 1000) % 2;
            AddBall(ballSlot.transform.GetChild(random));
        }
    }

    void AddBall(Transform slot)
    {
        int random = Random.Range(0, 1000) % ballGroups.Length;
        Instantiate(ballGroups[random], slot);
        maxBallsInGrounds[slotCounter / 4] += ballGroups[random].transform.childCount;
        slotCounter++;
    }

    void CreateTargets(int levelNumber)
    {
        for (int i = 0; i < myLevel.ballCounters.Length; i++)
        {
            BallCounter ballCounter = myLevel.ballCounters[i].GetComponent<BallCounter>();
            int num = Constants.MIN_TARGET + Random.Range(0,levelNumber);
            ballCounter.targetScore =
                Mathf.Clamp(num, Constants.MIN_TARGET, maxBallsInGrounds[i]);
            ballCounter.SetScoreOnText();
        }
    }

    void SetPickerMoveSpeed(int levelNumber)
    {
        float speed = Constants.START_MOVE_SPEED + Constants.SPEED_MULTIPLICATOR * levelNumber;

        GameFlowManager.Instance._picker.moveSpeed =
            Mathf.Clamp(speed, Constants.START_MOVE_SPEED,Constants.MAX_MOVE_SPEED);
        GameFlowManager.Instance._picker.rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
