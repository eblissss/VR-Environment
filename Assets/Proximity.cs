using UnityEngine;

public class Proximity : MonoBehaviour
{
    [Tooltip("The Animator to animate.")]
    public Animator Animator;
    [Tooltip("The proximal GameObject that affects the animator.")]
    public GameObject Affector;
    [Tooltip("The maximum distance that affects the animator.")]
    public float MaxDistance = 1.0f;
    [Tooltip("The animator Bool parameter to affect.")]
    public string Parameter = "";
    [Tooltip("The animator Bool parameter value to set.")]
    public bool Value = false;

    private void Update()
    {
        if (Animator == null || Affector == null) return;
        
        var animatorPosition = Animator.transform.position;
        var affectorPosition = Affector.transform.position;

        var currentDistance = Vector3.Distance(animatorPosition,
                                               affectorPosition);

        Animator.SetBool(Parameter, currentDistance > MaxDistance ? !Value : Value);
    }
}