using System;
using System.Collections;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 1f;
    public bool isStop = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (GameFlowManager.Instance.gameState == GameStates.Stop)
            {
                UIManager.Instance.SetDragToStartActive(false);
                GameFlowManager.Instance.gameState = GameStates.Running;
                isStop = false;
            }
        }

        if (GameFlowManager.Instance.gameState == GameStates.Stop)
        {
            return;
        }


        // keyboard input
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * 5f, 0, isStop ? 0f : moveSpeed);

        // touch support
        if (Input.touchCount > 0)
        {

            Touch t = Input.GetTouch(0);


            if (float.IsNaN(t.deltaPosition.x))
            {
                return;
            }
            rb.velocity = new Vector3(t.deltaPosition.x * 0.15f, 0, rb.velocity.z);
        }
    }

    private void LateUpdate()
    {
        UIManager.Instance.progressBarFillImage.fillAmount = transform.position.z / Constants.TOTAL_DISTANCE;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallCounterStop"))
        {
            isStop = true;
            Destroy(other);
            StartCoroutine(LoseChecker());
        }

        if (other.CompareTag("Finish"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true); // for some confetti fx
            isStop = true;
            UIManager.Instance.SetWin();
        }
    }

    // OnTriggerStay function detects which balls are inside of picker, and when we came to Ball Counter it pushes all the balls away.
    private void OnTriggerStay(Collider other)
    {
        if (isStop && other.gameObject.CompareTag("Ball"))
        {
            var velocity = other.GetComponent<Rigidbody>().velocity;
            velocity =
                new Vector3(velocity.x, velocity.y, 1f);
            other.GetComponent<Rigidbody>().velocity = velocity;
        }

    }

    IEnumerator LoseChecker()
    {
        yield return new WaitForSeconds(3f);
        if (isStop)
        {
            UIManager.Instance.SetLose();
        }
    }
}
