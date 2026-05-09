using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Task2Manager : MonoBehaviour
{
    public TMP_Text task2Text;
    public TMP_Text currentTaskText;
    public TMP_Text missionText;
    public TMP_Text timeText;

    public GameObject task2Objects;

    [Header("Waste Requirements Per Bin")]
    public int requiredPerBin = 3;

    [Header("Scene Finish")]
    public bool loadNextScene = false;
    public string nextSceneName;
    public float finishDelay = 2f;

    private int paperWasteCount = 0;
    private int plasticWasteCount = 0;
    private int metalWasteCount = 0;
    private int glassWasteCount = 0;

    private bool paperTruckDone = false;
    private bool plasticTruckDone = false;
    private bool metalTruckDone = false;
    private bool glassTruckDone = false;

    private bool wastePhaseCompleted = false;
    private bool truckPhaseStarted = false;
    private bool task2Completed = false;

    private TaskManager taskManager;

    void Start()
    {
        paperWasteCount = 0;
        plasticWasteCount = 0;
        metalWasteCount = 0;
        glassWasteCount = 0;

        paperTruckDone = false;
        plasticTruckDone = false;
        metalTruckDone = false;
        glassTruckDone = false;

        wastePhaseCompleted = false;
        truckPhaseStarted = false;
        task2Completed = false;

        taskManager = FindObjectOfType<TaskManager>();

        if (task2Objects != null)
        {
            task2Objects.SetActive(false);
        }

        UpdateUI();
    }

    void Update()
    {
        if (taskManager != null && taskManager.IsTask1Completed())
        {
            if (task2Objects != null && !task2Objects.activeSelf)
            {
                task2Objects.SetActive(true);
            }

            UpdateUI();
        }
    }

    public bool IsTruckPhaseStarted()
    {
        return truckPhaseStarted;
    }

    public void AddCorrectWasteToTask2(WasteType type)
    {
        if (task2Completed) return;
        if (wastePhaseCompleted) return;

        switch (type)
        {
            case WasteType.Paper:
                if (paperWasteCount < requiredPerBin)
                    paperWasteCount++;
                break;

            case WasteType.Plastic:
                if (plasticWasteCount < requiredPerBin)
                    plasticWasteCount++;
                break;

            case WasteType.Metal:
                if (metalWasteCount < requiredPerBin)
                    metalWasteCount++;
                break;

            case WasteType.Glass:
                if (glassWasteCount < requiredPerBin)
                    glassWasteCount++;
                break;
        }

        CheckWastePhaseComplete();
        UpdateUI();
    }

    void CheckWastePhaseComplete()
    {
        if (paperWasteCount >= requiredPerBin &&
            plasticWasteCount >= requiredPerBin &&
            metalWasteCount >= requiredPerBin &&
            glassWasteCount >= requiredPerBin)
        {
            wastePhaseCompleted = true;
            truckPhaseStarted = true;
            UpdateUI();
        }
    }

    public void MarkTruckCompleted(WasteType type)
    {
        if (task2Completed) return;
        if (!truckPhaseStarted) return;

        Debug.Log("Truck completed: " + type);

        switch (type)
        {
            case WasteType.Paper:
                if (!paperTruckDone) paperTruckDone = true;
                break;

            case WasteType.Plastic:
                if (!plasticTruckDone) plasticTruckDone = true;
                break;

            case WasteType.Metal:
                if (!metalTruckDone) metalTruckDone = true;
                break;

            case WasteType.Glass:
                if (!glassTruckDone) glassTruckDone = true;
                break;
        }

        UpdateUI();
        CheckTaskComplete();
    }

    void CheckTaskComplete()
    {
        if (paperTruckDone && plasticTruckDone && metalTruckDone && glassTruckDone)
        {
            task2Completed = true;

            if (currentTaskText != null)
                currentTaskText.text = "Current Task: Completed";

            if (missionText != null)
                missionText.text = "Mission Complete! All bins delivered to correct trucks.";

            if (task2Text != null)
                task2Text.text = "[DONE] Task 2: Completed";

            if (timeText != null)
                timeText.text = "";

            Debug.Log("Task 2 Completed!");
            Invoke(nameof(FinishScene), finishDelay);
        }
    }

    void FinishScene()
    {
        if (loadNextScene && !string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("Scene finished.");
        }
    }

    void UpdateUI()
    {
        if (taskManager == null || !taskManager.IsTask1Completed())
        {
            if (task2Text != null)
                task2Text.text = "";

            return;
        }

        if (task2Completed)
            return;

        if (currentTaskText != null)
            currentTaskText.text = "Current Task: Task 2";

        if (!truckPhaseStarted)
        {
            if (missionText != null)
                missionText.text = "Mission Started: Carry the ground waste to the correct bins.";

            if (task2Text != null)
            {
                task2Text.text =
                    "[ ] Task 2: " +
                    "P " + paperWasteCount + "/" + requiredPerBin + "   " +
                    "Pl " + plasticWasteCount + "/" + requiredPerBin + "   " +
                    "M " + metalWasteCount + "/" + requiredPerBin + "   " +
                    "G " + glassWasteCount + "/" + requiredPerBin;
            }
        }
        else
        {
            if (missionText != null)
                missionText.text = "Mission Started: Carry the bins to the correct trucks.";

            if (task2Text != null)
            {
                task2Text.text =
    "[ ] Task 2 Trucks: " +
    "P " + (paperTruckDone ? "DONE" : "WAIT") + "   " +
    "Pl " + (plasticTruckDone ? "DONE" : "WAIT") + "   " +
    "M " + (metalTruckDone ? "DONE" : "WAIT") + "   " +
    "G " + (glassTruckDone ? "DONE" : "WAIT");
            }
        }

        if (timeText != null)
            timeText.text = "";
    }
}