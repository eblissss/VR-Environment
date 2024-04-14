using System;
using Unity.Mathematics;
using UnityEngine;

public class SquidAnim : MonoBehaviour
{
    public Transform SourceCenter;
    public Transform Target;
    public float MeshSize = 1;
    public float Aggro = 5;
    
    public Vector3 ManualRotOffset;

    private Transform[] _children;
    private Vector3 _parentScale;

    private Vector3 _oPos;
    private Quaternion _oRot;
    private Vector3 _oSca;

    private const float SlerpSpeed = 0.05f;

    private void Start()
    {
        _parentScale = transform.lossyScale;
        _oPos = transform.position;
        _oRot = transform.rotation;
        _oSca = transform.localScale;
        
        _children = new Transform[transform.childCount];
        for (var i = 0; i < transform.childCount; i++)
        {
            _children[i] = transform.GetChild(i).GetComponent<Transform>();
        }
    }
    
    private void Update()
    {
        for (var i = 0; i < _children.Length; i++)
        {
            var child = _children[i];
            child.Rotate(new Vector3(
                             0, 
                             (float)Math.Sin(Time.time + (i * 6.28f / _children.Length)) / 200 *
                             (i + 1) / 5, 
                             (float)Math.Cos(Time.time + (i * 6.28f / _children.Length)) / 200 *
                             (i + 1) / 5));
        }
        
        var targetPos = Target.position;

        if (Vector3.Distance(targetPos, transform.position) < Aggro)
        {
            var startPos = SourceCenter.position;
         
            var midpoint = (targetPos + startPos) * 0.5f;
            var direction = Quaternion.LookRotation(targetPos - startPos, Vector3.forward);
            direction.eulerAngles += ManualRotOffset;

            var distance = Vector3.Distance(targetPos, startPos);
            var size = new Vector3((distance / _parentScale.x / MeshSize) + 0.1f, 1, 1);

            var slerpPos = Vector3.Slerp(transform.position, midpoint, SlerpSpeed);
            var slerpRot = Quaternion.Slerp(transform.rotation, direction, SlerpSpeed);
            var slerpSize = Vector3.Slerp(transform.localScale, size, SlerpSpeed);

            transform.position = slerpPos;
            transform.rotation = slerpRot;
            transform.localScale = slerpSize;
        }
        else
        {
            var slerpRot = Quaternion.Slerp(transform.rotation, _oRot, SlerpSpeed);
            var slerpPos = Vector3.Slerp(transform.position, _oPos, SlerpSpeed);
            var slerpSize = Vector3.Slerp(transform.localScale, _oSca, SlerpSpeed);
            
            transform.SetPositionAndRotation(slerpPos, slerpRot);
            transform.localScale = slerpSize;
        }
    }
}
