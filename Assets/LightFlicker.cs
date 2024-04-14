using UnityEngine;
using System.Collections.Generic;

public class LightFlickerEffect : MonoBehaviour {

    public Light Light;
    public float MinIntensity = 0f;
    public float MaxIntensity = 1f;

    [Range(1, 50)]
    public int Smoothing = 5;

    // Continuous average calculation via FIFO queue
    private Queue<float> _smoothQueue;
    private float _lastSum = 0;
    
    /// <summary>
    /// Reset the randomness and start again. You usually don't need to call
    /// this, deactivating/reactivating is usually fine but if you want a strict
    /// restart you can do.
    /// </summary>
    public void Reset() {
        _smoothQueue.Clear();
        _lastSum = 0;
    }

    private void Start() {
         _smoothQueue = new Queue<float>(Smoothing);

         if (Light == null) {
            Light = GetComponent<Light>();
         }
    }

    private void Update() {
        if (Light == null)
            return;
        
        while (_smoothQueue.Count >= Smoothing) {
            _lastSum -= _smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        var newVal = Random.Range(MinIntensity, MaxIntensity);
        _smoothQueue.Enqueue(newVal);
        _lastSum += newVal;

        // Calculate new smoothed average
        Light.intensity = _lastSum / _smoothQueue.Count;
    }
}