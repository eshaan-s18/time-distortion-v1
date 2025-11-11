using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LinearGrabLock : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private LinearSlider slider;

    private void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        slider = GetComponent<LinearSlider>();
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // keep handle locked to line even while grabbed
        grab.trackRotation = false;
        grab.trackPosition = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        grab.trackRotation = true;
        grab.trackPosition = true;
    }

    private void Update()
    {
        if (grab.isSelected)
        {
            // while grabbed, manually clamp position to the slider line
            slider.ApplyConstraint();
        }
    }
}
