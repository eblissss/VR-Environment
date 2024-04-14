using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DogWalk : MonoBehaviour
{
    public float Speed = 50f;
    public float RotSpeed = -40;
    
    public Animator Animator;
    public string Parameter;

    public GameObject Target;

    private CharacterController _controller;
    private Vector3 _savedDir;
    private bool _lastPara;

    // Start is called before the first frame update
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Animator.GetBool(Parameter))
        {
            if (!_lastPara)
            {
                _savedDir = transform.rotation.eulerAngles;
                _lastPara = true;
            }
            
            var targetDir = Target.transform.position - transform.position;
            var newDir = Vector3.RotateTowards(transform.forward, targetDir, Speed * 0.02f, 0.0f);
            var restrictedDir = new Vector3(newDir.x, _savedDir.y, newDir.z);
            restrictedDir.Normalize();
            transform.rotation = Quaternion.LookRotation(restrictedDir);
            transform.Rotate(Vector3.right, 90f, Space.Self);
        }
        else
        {
            if (_lastPara)
            {
                transform.eulerAngles = _savedDir;
                _lastPara = false;
            }
            
            transform.Rotate(Vector3.up, 0.02f * RotSpeed);
            var transForward = transform.TransformDirection(Vector3.forward);
            _controller.SimpleMove(Speed * 0.02f * transForward);
        }
    }
}
