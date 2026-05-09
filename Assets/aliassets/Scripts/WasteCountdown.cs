using UnityEngine;
using TMPro;

public class WasteCountdown : MonoBehaviour
{
    public float countdownTime = 8f;
    public TMP_Text countdownText;

    public float failY = -1f; // bu seviyenin altına düşerse kayıp say
    public float maxDistanceFromStop = 20f; // çok uzağa giderse kayıp say

    private float currentTime;
    private bool isCounting = false;
    private bool finished = false;

    private Transform stopPoint;

    public System.Action OnCountdownFinished;

    void Start()
    {
        currentTime = countdownTime;

        if (countdownText != null)
        {
            countdownText.text = "Time Left: -";
        }
    }

    void Update()
    {
        if (finished)
            return;

        // obje çok aşağı düştüyse fail
        if (transform.position.y < failY)
        {
            FailWaste();
            return;
        }

        // stop point verildiyse ve çok uzağa gittiyse fail
        if (stopPoint != null)
        {
            float distance = Vector3.Distance(transform.position, stopPoint.position);
            if (distance > maxDistanceFromStop)
            {
                FailWaste();
                return;
            }
        }

        if (!isCounting)
            return;

        currentTime -= Time.deltaTime;

        if (countdownText != null)
        {
            countdownText.text = "Time Left: " + Mathf.CeilToInt(currentTime).ToString();
        }

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            FailWaste();
        }
    }

    public void StartCountdown()
    {
        if (finished) return;

        currentTime = countdownTime;
        isCounting = true;

        if (countdownText != null)
        {
            countdownText.text = "Time Left: " + Mathf.CeilToInt(currentTime).ToString();
        }
    }

    public void StopCountdown()
    {
        isCounting = false;

        if (countdownText != null)
        {
            countdownText.text = "Time Left: -";
        }
    }

    public void SetStopPoint(Transform point)
    {
        stopPoint = point;
    }

    private void FailWaste()
    {
        if (finished) return;

        finished = true;
        isCounting = false;

        if (countdownText != null)
        {
            countdownText.text = "Time Left: -";
        }

        OnCountdownFinished?.Invoke();
    }
}