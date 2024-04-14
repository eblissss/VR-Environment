using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticInteract : MonoBehaviour
{
    [Range(0, 1)] public float Intensity;
    public float Duration;

    private XRBaseController _controller;

    private void Start()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.activated.AddListener(SetController);
        interactable.deactivated.AddListener(UnsetController);
    }

    private void SetController(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            _controller = controllerInteractor.xrController;
        }
    }

    private void UnsetController(DeactivateEventArgs args)
    {
        _controller = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_controller == null) return;
        
        if (collision.relativeVelocity.magnitude > 2 && Intensity > 0)
            _controller.SendHapticImpulse(Intensity, Duration);
    }
}
