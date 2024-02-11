using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class Duck : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidBody;

    private float maxSpeed = 15f;
    private float glideRatio = 10f;

    private float maxTurningRateDegrees = 10f;

    private float maxForce = 10f;

    private string animIsFlying = "isFlying";
    private string animHasDiedInAir = "hasDiedInAir";
    private string animIsLanding = "isLanding";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        // SetStartAnimation();
        // StartCoroutine(AnimateDuck());
    }

   

    public void LandOnWater(Vector3 targetPosition)
    {
        FlyTowards(targetPosition);

        // Wait until within landing distance
        while (Vector3.Distance(transform.position, targetPosition) > 20f)
        {
            // Do nothing
        }

        isLandingAnim();
        while (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            // Do nothing
        }
        rigidBody.velocity = Vector3.zero;
    }
  
    void SetStartAnimation()
    {
        // Check if the distance to the ground is larger than 0.1
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10f))
        {
            if (hit.distance > 0.1f)
            {
                // Set the flying animation
                animator.SetBool(animIsFlying, true);

                // Add forward speed
                rigidBody.AddForce(transform.forward * maxSpeed, ForceMode.Acceleration);
            }
        }
    }

    void TakeOff()
    {
        animator.SetBool(animIsFlying, true);
    }

    void isFlyingAnim()
    {
        animator.SetBool(animIsFlying, true);
    }

    void isLandingAnim()
    {
        animator.SetBool(animIsFlying, false);
        animator.SetBool(animIsLanding, true);
    }

    void Glide()
    {
        // Calculate the glide force
        Vector3 glideForce = Vector3.up * Physics.gravity.magnitude * glideRatio;

        // Apply the glide force to the duck
        GetComponent<Rigidbody>().AddForce(glideForce, ForceMode.Acceleration);
    }

    void TurnLeft()
    {
        // Calculate the turning force
        Vector3 turningForce = -transform.right * maxForce;

        // Apply the turning force to the duck
        GetComponent<Rigidbody>().AddForce(turningForce, ForceMode.Acceleration);
    }

    void FlyTowards(Vector3 targetPosition)
    {
        Vector3 velocity = (targetPosition - transform.position).normalized * maxSpeed;
        GetComponent<Rigidbody>().velocity = velocity;
        isFlyingAnim();
    }




    void TurnRight()
    {
        // Calculate the turning force
        Vector3 turningForce = transform.right * maxForce;

        // Apply the turning force to the duck
        GetComponent<Rigidbody>().AddForce(turningForce, ForceMode.Acceleration);
    }



    void DieInAir()
    {
        animator.SetBool(animIsFlying, false);
        animator.SetBool(animHasDiedInAir, true);
    }

    IEnumerator AnimateDuck()
    {
        // Start with flying animation
        animator.SetBool(animIsFlying, true);
        yield return new WaitForSeconds(5f); // Time for flying animation to complete

        // Transition to landing animation
        animator.SetBool(animIsFlying, false);
        animator.SetBool(animIsLanding, true);
        yield return new WaitForSeconds(5f); // Time for landing animation to complete

        // Transition to flying animation again
        animator.SetBool(animIsLanding, false);
        animator.SetBool(animIsFlying, true);
        yield return new WaitForSeconds(5f); // Time for flying animation to complete

        // Transition to has died in air animation
        animator.SetBool(animIsFlying, false);
        animator.SetBool(animHasDiedInAir, true);
        yield return new WaitForSeconds(5f); // Time for has died in air animation to complete

        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
        
    }
}
