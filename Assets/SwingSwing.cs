using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwingSwing : MonoBehaviour
{
    public Transform InteractorTransform;
    public float Multiplier = 1;
    public Transform PivotPoint;
    
    private float _lastFrameAngle; 

    private void Start() {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(Selected);
        interactable.selectExited.AddListener(Deselected);
    }

    private void FixedUpdate()
    {
        if (InteractorTransform == null) return;
        
        var angle = Vector3.SignedAngle(InteractorTransform.position - transform.position, InteractorTransform.forward, Vector3.up);
        var delta = angle - _lastFrameAngle;
        transform.RotateAround(PivotPoint.position, transform.up, delta*Multiplier);           
        _lastFrameAngle = angle;
    }

    private void Selected(SelectEnterEventArgs arguments) {        
        InteractorTransform = arguments.interactorObject.transform;
        _lastFrameAngle = Vector3.SignedAngle(InteractorTransform.position - transform.position, InteractorTransform.forward, Vector3.up);        
    }

    private void Deselected(SelectExitEventArgs arguments) {        
        InteractorTransform = null;        
    }
}
