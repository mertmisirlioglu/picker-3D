using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    [SerializeField] public int targetScore;
    private int myScore;

    private List<GameObject> balls;
    public bool isPassed;
    public GameObject roadBlock;

    void Awake()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        balls = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            balls.Add(collision.gameObject);
            myScore++;
            SetScoreOnText();

            if (myScore >= targetScore && !isPassed)
            {
                StartCoroutine(PassAnimation());
            }

            if (isPassed)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public void SetScoreOnText()
    {
        scoreText.SetText(targetScore + "/" + myScore);
    }

    IEnumerator PassAnimation()
    {
        isPassed = true;

        foreach (var ball in balls)
        {
            ball.transform.DOScale(Vector3.zero, 0.5f).OnComplete((() => Destroy(ball)));
        }

        yield return new WaitForSeconds(0.5f);
        scoreText.enabled = false;

        transform.DOMoveY(0.05f, 0.5f).OnComplete(() =>
        {
            transform.DOMoveY(0, 0.2f).OnComplete(() =>
            {
                roadBlock.GetComponent<Rigidbody>().AddForce(new Vector2(Random.Range(-20,20), 20), ForceMode.Impulse);
                GameFlowManager.Instance._picker.isStop = false;
            });
        });

        GetComponent<MeshRenderer>().material = GameObject.Find("Ground").GetComponent<MeshRenderer>().material;



    }
}
