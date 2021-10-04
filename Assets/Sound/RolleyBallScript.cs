using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolleyBallScript : MonoBehaviour
{
    public AudioSource rollingEffect;
    public AudioSource knockEffect;

    public List<Collider> contacts;

    private void Awake() {
        contacts = new List<Collider>();
    }

    Rigidbody rb;
    new public Rigidbody rigidbody {
        get => rb ? rb : (rb = GetComponent<Rigidbody>());
    }

    private void Update() {
        rollingEffect.volume = (contacts.Count > 0 ? 1f : 0f) *
            Mathf.Min(1f, rigidbody.velocity.magnitude / 5);
    }

    private void OnCollisionEnter(Collision collision) {
        if (!contacts.Contains(collision.collider))
            contacts.Add(collision.collider);

        knockEffect.volume = collision.relativeVelocity.magnitude / 20;
        Debug.Log(collision.relativeVelocity.magnitude);
        knockEffect.Play();
    }

    private void OnCollisionExit(Collision collision) {
        contacts.Remove(collision.collider);
    }
}
