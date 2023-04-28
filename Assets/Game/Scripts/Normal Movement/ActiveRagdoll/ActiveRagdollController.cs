using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdollController : MonoBehaviour
{

    public float speed;
    public float straftSpeed;

    public float jumpForce;

    public Rigidbody hips;
    public ConfigurableJoint hipsJoint;

    public float GroundCount;
    public GroundDetection[] gd;
    public Animator anim;

    public Transform lockOnTarget;
    public Transform BasedPosition;
    
    void Start()
    {
        
    }

    float horizontal;
    float vertical;
    private void FixedUpdate()
    {
        
        GroundCount = 0;
        for (int i = 0; i < gd.Length; i++)
        {

            GroundCount += gd[i].grounded ? 0.5f : 0;
        }

  
        BasedPosition.forward = lockOnTarget.transform.forward;
        hipsJoint.targetRotation = Quaternion.Euler(0,0, -BasedPosition.eulerAngles.y);

        if (vertical != 0)
        {
            hips.AddForce(-hips.transform.up * speed * vertical);
        }

        if (horizontal != 0)
        {
            hips.AddForce(hips.transform.right * straftSpeed * horizontal);
        }

    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hips.AddForce(transform.up * jumpForce * GroundCount, ForceMode.Impulse);
           
        }
    }
}
