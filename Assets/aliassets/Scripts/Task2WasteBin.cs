using UnityEngine;

public class Task2WasteBin : MonoBehaviour
{
    public WasteType acceptedType;
    public Task2CarryBin linkedCarryBin;

    private void OnTriggerEnter(Collider other)
    {
        WasteItem item = other.GetComponent<WasteItem>();
        if (item == null) return;

        if (item.wasteType == acceptedType)
        {
            if (linkedCarryBin != null)
            {
                linkedCarryBin.AddWaste();
            }

            Destroy(other.gameObject);

            Debug.Log("Task 2 correct waste: " + item.wasteType);
        }
        else
        {
            Debug.Log("Wrong bin for Task 2!");
        }
    }
}