using UnityEngine;

public class AnvilSound : MonoBehaviour
{
    public AudioSource AudioToPlay;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.2)
            AudioToPlay.Play();
    }
}
