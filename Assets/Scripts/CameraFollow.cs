using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 target_Offset;

    void FixedUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position+target_Offset, 0.1f);
        }
    }
}
