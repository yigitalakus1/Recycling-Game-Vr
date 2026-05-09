using UnityEngine;
using System.Collections;

public class BinZone : MonoBehaviour
{
    public WasteType acceptedType;
    public Transform dropPoint;

    private bool isProcessing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isProcessing) return;

        WasteItem item = other.GetComponent<WasteItem>();
        if (item == null) return;

        if (item.wasteType == acceptedType)
        {
            StartCoroutine(PullIntoBin(other.gameObject));
        }
        else
        {
            Debug.Log("Wrong bin!");
        }
    }

    private IEnumerator PullIntoBin(GameObject wasteObject)
    {
        isProcessing = true;

        yield return new WaitForSeconds(0.15f);

        Rigidbody rb = wasteObject.GetComponent<Rigidbody>();
        WasteCountdown countdown = wasteObject.GetComponent<WasteCountdown>();

        if (countdown != null)
        {
            countdown.StopCountdown();
        }

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        float duration = 1.0f;
        float elapsed = 0f;

        Vector3 startPos = wasteObject.transform.position;
        Quaternion startRot = wasteObject.transform.rotation;

        Vector3 targetPos = dropPoint.position;
        Quaternion targetRot = dropPoint.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            wasteObject.transform.position = Vector3.Lerp(startPos, targetPos, smoothT);
            wasteObject.transform.rotation = Quaternion.Slerp(startRot, targetRot, smoothT);

            yield return null;
        }

        wasteObject.transform.position = targetPos;
        wasteObject.transform.rotation = targetRot;

        yield return new WaitForSeconds(0.1f);

        TaskManager taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            taskManager.AddCorrectWaste();
        }

        Destroy(wasteObject);

        ConveyorSpawner spawner = FindObjectOfType<ConveyorSpawner>();
        if (spawner != null)
        {
            spawner.ClearCurrentWasteAndSpawnNext();
        }

        isProcessing = false;
    }
}