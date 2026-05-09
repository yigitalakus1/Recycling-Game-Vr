using UnityEngine;
using TMPro;

public class ConveyorSpawner : MonoBehaviour
{
    public GameObject[] wastePrefabs;

    public Transform glassSpawnPoint;
    public Transform metalSpawnPoint;
    public Transform paperSpawnPoint;
    public Transform plasticSpawnPoint;

    public Transform glassStopPoint;
    public Transform metalStopPoint;
    public Transform paperStopPoint;
    public Transform plasticStopPoint;

    public TMP_Text countdownText;

    private GameObject currentWaste;
    private bool taskActive = true;

    void Start()
    {
        SpawnWaste();
    }

    public void SpawnWaste()
    {
        if (!taskActive)
            return;

        if (currentWaste != null)
            return;

        int randomIndex = Random.Range(0, wastePrefabs.Length);
        GameObject selectedPrefab = wastePrefabs[randomIndex];

        WasteItem wasteItem = selectedPrefab.GetComponent<WasteItem>();
        if (wasteItem == null)
        {
            Debug.LogWarning("WasteItem component eksik: " + selectedPrefab.name);
            return;
        }

        Transform selectedSpawnPoint = GetSpawnPoint(wasteItem.wasteType);
        Transform selectedStopPoint = GetStopPoint(wasteItem.wasteType);

        if (selectedSpawnPoint == null || selectedStopPoint == null)
        {
            Debug.LogWarning("Spawn veya Stop point bulunamadı: " + selectedPrefab.name);
            return;
        }

        currentWaste = Instantiate(
            selectedPrefab,
            selectedSpawnPoint.position,
            selectedSpawnPoint.rotation
        );

        ConveyorMover mover = currentWaste.GetComponent<ConveyorMover>();
        WasteCountdown countdown = currentWaste.GetComponent<WasteCountdown>();

        if (mover != null)
        {
            mover.targetPoint = selectedStopPoint;

            mover.OnReachedTarget += () =>
            {
                if (countdown != null)
                {
                    countdown.countdownText = countdownText;
                    countdown.SetStopPoint(selectedStopPoint);
                    countdown.StartCountdown();
                }
            };
        }

        if (countdown != null)
        {
            countdown.OnCountdownFinished += () =>
            {
                if (currentWaste != null)
                {
                    Destroy(currentWaste);
                    currentWaste = null;
                }

                SpawnWaste();
            };
        }
    }

    private Transform GetSpawnPoint(WasteType wasteType)
    {
        switch (wasteType)
        {
            case WasteType.Glass:
                return glassSpawnPoint;
            case WasteType.Metal:
                return metalSpawnPoint;
            case WasteType.Paper:
                return paperSpawnPoint;
            case WasteType.Plastic:
                return plasticSpawnPoint;
            default:
                return null;
        }
    }

    private Transform GetStopPoint(WasteType wasteType)
    {
        switch (wasteType)
        {
            case WasteType.Glass:
                return glassStopPoint;
            case WasteType.Metal:
                return metalStopPoint;
            case WasteType.Paper:
                return paperStopPoint;
            case WasteType.Plastic:
                return plasticStopPoint;
            default:
                return null;
        }
    }

    public void ClearCurrentWasteAndSpawnNext()
    {
        if (!taskActive)
            return;

        currentWaste = null;
        SpawnWaste();
    }

    public void StopTask()
    {
        taskActive = false;

        if (currentWaste != null)
        {
            Destroy(currentWaste);
            currentWaste = null;
        }

        if (countdownText != null)
        {
            countdownText.text = "Time Left: -";
        }
    }
}