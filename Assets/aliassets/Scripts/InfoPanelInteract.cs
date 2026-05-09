using UnityEngine;

public class InfoPanelInteract : MonoBehaviour
{
    public float enlargeMultiplier = 1.8f;
    public float moveSpeed = 5f;

    public float moveUpAmount = 0.5f;
    public float moveForwardAmount = 0.5f;

    private bool isEnlarged = false;

    private Vector3 normalScale;
    private Vector3 enlargedScale;
    private Vector3 targetScale;

    private Vector3 normalPosition;
    private Vector3 targetPosition;

    void Start()
    {
        normalScale = transform.localScale;
        enlargedScale = normalScale * enlargeMultiplier;
        targetScale = normalScale;

        normalPosition = transform.position;
        targetPosition = normalPosition;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * moveSpeed
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * moveSpeed
        );
    }

    public void TogglePanel()
    {
        isEnlarged = !isEnlarged;

        if (isEnlarged)
        {
            targetScale = enlargedScale;
            targetPosition = normalPosition
                + (Vector3.up * moveUpAmount)
                - (transform.forward * moveForwardAmount);
        }
        else
        {
            targetScale = normalScale;
            targetPosition = normalPosition;
        }
    }
}