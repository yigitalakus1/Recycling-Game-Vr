using UnityEngine;

public class TruckDeliveryZone : MonoBehaviour
{
    public WasteType acceptedType;
    public TruckController truck;
    public Task2Manager task2Manager;

    private bool delivered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (delivered) return;

        Debug.Log("Zone touched by: " + other.name);

        Task2CarryBin carryBin = other.GetComponent<Task2CarryBin>();

        if (carryBin == null)
            carryBin = other.GetComponentInParent<Task2CarryBin>();

        if (carryBin == null)
        {
            Debug.Log("No Task2CarryBin found.");
            return;
        }

        if (task2Manager == null)
        {
            Debug.Log("Task2Manager missing.");
            return;
        }

        if (!task2Manager.IsTruckPhaseStarted())
        {
            Debug.Log("Truck phase not started.");
            return;
        }

        Debug.Log("Carry bin type: " + carryBin.GetWasteType());
        Debug.Log("Accepted type: " + acceptedType);
        Debug.Log("Carry bin ready: " + carryBin.IsReadyForTruck());

        if (!carryBin.IsReadyForTruck())
        {
            Debug.Log("Carry bin is not full yet.");
            return;
        }

        if (carryBin.GetWasteType() != acceptedType)
        {
            Debug.Log("Wrong truck for this bin.");
            return;
        }

        delivered = true;

        Debug.Log("Correct bin delivered: " + acceptedType);

        task2Manager.MarkTruckCompleted(acceptedType);

        if (truck != null)
        {
            truck.SendTruck();
        }

        carryBin.gameObject.SetActive(false);
    }
}