using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public BallController inputActions;
    public Transform steeringReference;

    public Vector3 forwardRoll {
        get => steeringReference ? steeringReference.right : Vector3.right;
    }

    public Vector3 sidewaysRoll {
        get => Quaternion.Euler(0, 90, 0) * forwardRoll;
    }

    //public float maxAngularVelocity = 100;
    public float torqueForce = 1;

    private bool forward = false, backward = false, right = false, left = false;

    //private Rigidbody rb;
    public Rigidbody ballRigidbody;// { 
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
        var torque = ballRigidbody.angularVelocity;

        if (left) {
            ballRigidbody.AddTorque(sidewaysRoll * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        if (right) {
            ballRigidbody.AddTorque(-sidewaysRoll * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        if (backward) {
            ballRigidbody.AddTorque(-forwardRoll * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        if (forward) {
            ballRigidbody.AddTorque(forwardRoll * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        //if (!(left || right || backward || forward)) {
        //    ballRigidbody.angularVelocity *= Mathf.Min(0f, 1f - (Time.fixedDeltaTime));
        //}
    }
}
