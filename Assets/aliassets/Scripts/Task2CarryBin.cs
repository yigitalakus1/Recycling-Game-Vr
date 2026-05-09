using UnityEngine;

public class Task2CarryBin : MonoBehaviour
{
    public WasteType wasteType;
    public int requiredWasteCount = 3;

    private int currentWasteCount = 0;

    public bool IsReadyForTruck()
    {
        return currentWasteCount >= requiredWasteCount;
    }

    public WasteType GetWasteType()
    {
        return wasteType;
    }

    public void AddWaste()
    {
        if (currentWasteCount < requiredWasteCount)
        {
            currentWasteCount++;
            Debug.Log(gameObject.name + " waste count: " + currentWasteCount + "/" + requiredWasteCount);
        }
    }

    public int GetCurrentWasteCount()
    {
        return currentWasteCount;
    }

    public int GetRequiredWasteCount()
    {
        return requiredWasteCount;
    }
}