using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdollController : MonoBehaviour
{

    public float speed;
    public float straftSpeed;

    public float jumpForce;

    public Rigidbody hips;

    public float GroundCount;
    public GroundDetection[] gd;
    public Animator anim;
    
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
