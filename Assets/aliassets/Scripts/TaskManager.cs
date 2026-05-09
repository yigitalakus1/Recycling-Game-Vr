using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public TMP_Text currentTaskText;
    public TMP_Text task1Text;

    public int requiredCount = 5;

    private int currentCount = 0;
    private bool task1Completed = false;

    void Start()
    {
        currentCount = 0;
        task1Completed = false;
        UpdateUI();
    }

    public void AddCorrectWaste()
    {
        if (task1Completed) return;

        currentCount++;

        if (currentCount >= requiredCount)
        {
            task1Completed = true;

            ConveyorSpawner spawner = FindObjectOfType<ConveyorSpawner>();
            if (spawner != null)
            {
                spawner.StopTask();
            }
        }

        UpdateUI();
    }

    public bool IsTask1Completed()
    {
        return task1Completed;
    }

    void UpdateUI()
    {
        if (task1Completed)
        {
            currentTaskText.text = "Current Task: Task 2";
            task1Text.text = "[DONE] Task 1: Sort conveyor waste into correct bins";
        }
        else
        {
            currentTaskText.text = "Current Task: Task 1";
            task1Text.text = "[WAIT] Task 1: Sort conveyor waste into correct bins (" + currentCount + "/" + requiredCount + ")";
        }
    }
}