using UnityEngine;

public class TruckDropZone : MonoBehaviour
{
    public TruckController truck;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered truck zone: " + other.name);

        Task2CarryBin carryBin = other.GetComponentInParent<Task2CarryBin>();
        if (carryBin == null)
        {
            Debug.Log("No Task2CarryBin found.");
            return;
        }

        if (truck == null)
        {
            Debug.Log("Truck reference is null.");
            return;
        }

        Task2Manager manager = FindObjectOfType<Task2Manager>();
        if (manager == null)
        {
            Debug.Log("Task2Manager not found.");
            return;
        }

        if (!manager.IsTruckPhaseStarted())
        {
            Debug.Log("Truck phase has not started yet.");
            return;
        }

        Debug.Log("CarryBin Type: " + carryBin.GetWasteType());
        Debug.Log("Truck Type: " + truck.acceptedType);
        Debug.Log("CarryBin Ready: " + carryBin.IsReadyForTruck());

        if (carryBin.GetWasteType() == truck.acceptedType && carryBin.IsReadyForTruck())
        {
            truck.SendTruck();
            manager.MarkTruckCompleted(truck.acceptedType);
            Debug.Log("Correct carry bin delivered to truck: " + truck.acceptedType);
        }
        else
        {
            Debug.Log("Wrong or incomplete carry bin for truck.");
        }
    }
}