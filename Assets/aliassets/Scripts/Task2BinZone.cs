using UnityEngine;

public class Task2BinZone : MonoBehaviour
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

            Task2Manager manager = FindObjectOfType<Task2Manager>();
            if (manager != null)
            {
                manager.AddCorrectWasteToTask2(item.wasteType);
            }

            Destroy(other.gameObject);

            Debug.Log("Correct waste added: " + item.wasteType);
        }
        else
        {
            Debug.Log("Wrong bin!");
        }
    }
}