using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
// Yeni sürümde bu satır hayati önem taşıyor:
using UnityEngine.XR.Interaction.Toolkit.Interactables; 

public class DisableConveyorOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private ConveyorMover conveyorMover;

    void Awake()
    {
        // Yeni sürümdeki objeyi buluyoruz
        grabInteractable = GetComponent<XRGrabInteractable>();
        conveyorMover = GetComponent<ConveyorMover>();
    }

    void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (conveyorMover != null)
        {
            conveyorMover.enabled = false;
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (conveyorMover != null)
        {
            conveyorMover.enabled = true;
        }
    }
}