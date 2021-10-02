using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float maxAngularVelocity = 100;

    public float torqueForce = 1;

    private Rigidbody rb;
    public Rigidbody rigidbody { 
        get { if (rb == null) rb = transform.GetComponent<Rigidbody>(); return rb; } 
        set => rb = value;    
    }

    void FixedUpdate()
    {
        var torque = rigidbody.angularVelocity;

        if (Input.GetKey(KeyCode.LeftArrow) && torque.z > -maxAngularVelocity) {
            rigidbody.AddRelativeTorque(-Vector3.Forward * torqueForce * Time.FixedDelta, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.RightArrow) && torque.z < maxAngularVelocity) {
            rigidbody.AddRelativeTorque(Vector3.Forward * torqueForce * Time.FixedDelta, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.DownArrow) && torque.x > -maxAngularVelocity) {
            rigidbody.AddRelativeTorque(Vector3.Right * torqueForce * Time.FixedDelta, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.UpArrow) && torque.x < maxAngularVelocity) {
            rigidbody.AddRelativeTorque(Vector3.Right * torqueForce * Time.FixedDelta, ForceMode.Force);
        }
    }
}
