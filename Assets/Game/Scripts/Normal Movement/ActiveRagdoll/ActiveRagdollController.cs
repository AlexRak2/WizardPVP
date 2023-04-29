using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TMP_Text speedText;
    
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
        Vector3 targetVel = new Vector3(0, hips.velocity.y, 0);
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

        hips.velocity = targetVel;

        if (vertical == 0 && horizontal == 0 && GroundCount > 0)
        {
            Vector3 counterVel = new Vector3(hips.velocity.x, 0, hips.velocity.z);
            //hips.AddForce(-counterVel, ForceMode.VelocityChange);
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);

        speedText.text = new Vector3(hips.velocity.x, 0, hips.velocity.z).magnitude.ToString();
        if (Input.GetKeyDown(KeyCode.Space) && GroundCount>0)
        {
            hips.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
