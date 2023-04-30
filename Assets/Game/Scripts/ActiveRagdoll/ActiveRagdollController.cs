using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class ActiveRagdollController : NetworkBehaviour
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

    private ClientStats clientStats;

    void Start()
    {
        clientStats = GetComponent<ClientStats>();    
    }

    float horizontal;
    float vertical;
    private void FixedUpdate()
    {
        if (!isOwned) return;
        if (clientStats.isDead) return;

        GroundCount = 0;
        for (int i = 0; i < gd.Length; i++)
        {

            GroundCount += gd[i].grounded ? 0.5f : 0;
        }

        
        BasedPosition.forward = lockOnTarget.transform.forward;
        if (Input.GetKey(KeyCode.LeftShift) || (horizontal != 0 || vertical != 0))
        {
            hipsJoint.targetRotation = Quaternion.Euler(0, 0, -BasedPosition.eulerAngles.y);
        }
        Vector3 targetVel = Vector3.zero;
        if (vertical != 0)
        {
            //hips.AddForce(-hips.transform.up * speed * vertical * Time.fixedDeltaTime, ForceMode.VelocityChange);
            targetVel += -hips.transform.up * speed * vertical;
        }

        if (horizontal != 0)
        {
            //hips.AddForce(hips.transform.right * straftSpeed * horizontal * Time.fixedDeltaTime, ForceMode.VelocityChange);
            targetVel += hips.transform.right * straftSpeed * horizontal;
        }
        targetVel = Vector3.ClampMagnitude(targetVel, speed);

        targetVel.y = hips.velocity.y;

        hips.velocity = targetVel;
    }

    private void Update()
    {
        if (!isOwned) return;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
        
        if (Input.GetKeyDown(KeyCode.Space) && GroundCount>0)
        {
            hips.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
