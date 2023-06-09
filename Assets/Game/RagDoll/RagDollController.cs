using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 5f;
    [SerializeField] public ConfigurableJoint hipJoint;
    [SerializeField] public Rigidbody hip;

    [SerializeField] private Animator targetAnimator;

    private bool walk = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            //hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            hip.AddForce(direction * speed);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hip.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }

        targetAnimator.SetFloat("Vertical", direction.z);
        targetAnimator.SetFloat("Horizontal", direction.x);
    }

}