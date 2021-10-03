using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public BallController inputActions;

    //public float maxAngularVelocity = 100;
    public float torqueForce = 1;

    private bool forward = false, backward = false, right = false, left = false;

    //private Rigidbody rb;
    new public Rigidbody rigidbody;// { 
    //    get { if (rb == null) rb = transform.GetComponent<Rigidbody>(); return rb; } 
    //    set => rb = value;    
    //}

    private void Awake()
    {
        inputActions = new BallController();

        inputActions.Player.Speed.performed += (val) => {
            forward = (val.ReadValue<float>() > 0);
            backward = val.ReadValue<float>() < 0;
        };

        inputActions.Player.Speed.canceled += (val) => {
            forward = false; backward = false;
        };

        inputActions.Player.Steering.performed += (val) => {
            right = val.ReadValue<float>() < 0;
            left = val.ReadValue<float>() > 0;
        };

        inputActions.Player.Steering.canceled += (val) => {
            right = false; left = false;
        };
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    void FixedUpdate()
    {
        var torque = rigidbody.angularVelocity;

        if (left) {
            rigidbody.AddTorque(new Vector3(0,0,-1) * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        if (right) {
            rigidbody.AddTorque(new Vector3(0,0,1) * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        if (backward) {
            rigidbody.AddTorque(new Vector3(-1,0,0) * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        if (forward) {
            rigidbody.AddTorque(new Vector3(1,0,0) * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }
}
