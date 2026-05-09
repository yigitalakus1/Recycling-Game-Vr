using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    public Transform targetPoint;
    public float speed = 2f;

    private bool hasReachedTarget = false;

    public System.Action OnReachedTarget;

    void Update()
    {
        if (targetPoint == null || hasReachedTarget)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            hasReachedTarget = true;
            OnReachedTarget?.Invoke();
        }
    }
}