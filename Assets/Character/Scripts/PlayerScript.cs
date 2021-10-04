using UnityEngine;
using System.Collections;

public class PlayerScript: MonoBehaviour
{
    static PlayerScript playerInstance = null;

    private Controller mc = null;
    public Controller movementController {
        get => mc == null ? (mc = GetComponent<Controller>()) : mc;
    }

    [ReadOnly]
    public float maxLife = 2.0f;
    [SerializeField]
    private float life = 2.0f;
    public float Life {
        get => life;
        set { life = value; UpdateModel(); }
    }

    public AnimationCurve sizeOverLife;
    public AnimationCurve massOverLife;
    public Gradient colorOverLife;

    public Transform characterModel;

    private void Awake() {
        //if (playerInstance == null) playerInstance = this;
        //else gameObject.SetActive(false);
    }

    private void OnValidate() {
        if (maxLife != life) {
            if(!Application.isPlaying)
                maxLife = life;
            UpdateModel();
        }
    }

    private void UpdateModel()
    {
        characterModel.localScale = Vector3.one * sizeOverLife.Evaluate(Life);

        Rigidbody rb = movementController.ballRigidbody;
        float change = massOverLife.Evaluate(Life) / rb.mass;
        rb.mass = massOverLife.Evaluate(Life);

        if (Application.isPlaying) {
            characterModel.GetComponent<MeshRenderer>().material.color =
                colorOverLife.Evaluate(Life / maxLife);

            rb.velocity *= change;
        }
    }
}
