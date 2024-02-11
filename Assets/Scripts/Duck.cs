using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Duck : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidBody;

    private float maxSpeed = 15f;
    private float glideRatio = 10f;

    private float maxForce = 10f;

    private enum DuckState
    {
        Flying,
        Dead,
        Swimming,
        Walking
    }

    private DuckState duckState = DuckState.Flying;

    private string animFlyFromWater = "FlyFromWater";
    private string animHasDiedInAir = "DieFlying";
    private string animLandOnWater = "LandOnWater";
    private string animFlapSpeed = "FlapSpeed";
    private string animFlyFromGround = "FlyFromGround";
    private string animDieOnGround = "DieOnGround";
    private string animLandOnGround = "LandOnGround";
    private string animDieOnWater = "DieOnWater";

    // Start is called before the first frame update
    void Start()
    {
        SetFlapSpeed(3f);
        FlyToWaypoint(new Vector3(0, 0, 0));
    }

    // public new void DestroyTarget()
    // {
    //     if (DuckState.Dead == duckState) { return; }
    //     if (DuckState.Flying == duckState)
    //     {
    //         HasDiedInAirAnim();
    //     }
    //     else if (DuckState.Walking == duckState)
    //     {
    //         DieOnGroundAnim();
    //     }
    //     else if (DuckState.Swimming == duckState)
    //     {
    //         DieOnWaterAnim();
    //     }
    
    //     WaitForAnimationToFinish().Wait();
    //     targetStatistics.TargetHit();
    //     duckState = DuckState.Dead;
    //     Destroy(gameObject);
    // }

    private async Task WaitForAnimationToFinish()
    {
        Animator animator = GetComponentInChildren<Animator>();
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        await Task.Delay(TimeSpan.FromSeconds(animationLength));
    }

    public void SetFlapSpeed(float speed)
    {
        GetComponentInChildren<Animator>().SetFloat(animFlapSpeed, speed);
    }

    void FlyFromWaterAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger(animFlyFromWater);
    }

    void FLyFromGroundAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger(animFlyFromGround);
    }

    void LandOnWaterAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger(animLandOnWater);
    }

    void HasDiedInAirAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger(animHasDiedInAir);
    }

    void DieOnGroundAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger(animDieOnGround);
    }

    void LandOnGroundAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger(animLandOnGround);
    }

    void DieOnWaterAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger(animDieOnWater);
    }

    void FlyFromGroundAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger(animFlyFromGround);
    }

    void SetVelocity(Vector3 velocity)
    {
        rigidBody = GetComponent<Rigidbody>();
        Debug.Log("Setting velocity to " + velocity + " for " + rigidBody);
        rigidBody.velocity = velocity;
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

    void TakeOff()
    {
        if (DuckState.Flying == duckState || DuckState.Dead == duckState) { return; }
        if (DuckState.Swimming == duckState)
        {
            FlyFromWaterAnim();
        }
        else if (DuckState.Walking == duckState)
        {
            FlyFromGroundAnim();
        }

        // Calculate the takeoff force with a forward angle
        Vector3 takeoffForce = (transform.forward + Vector3.up) * maxForce;
        // Apply the takeoff force to the duck
        GetComponent<Rigidbody>().AddForce(takeoffForce, ForceMode.Acceleration);
        duckState = DuckState.Flying;
    }

    public void PlannedFlyToAndLand(Vector3 targetPosition, Quaternion targetDirection)
    {
        ShouldBeFlying();
        Transform plan = PlanLandingSequence(targetPosition, targetDirection);
        FlyToWaypoint(plan.position);
        LandAtTarget(targetPosition);
    }


    private Transform PlanLandingSequence(Vector3 targetPosition, Quaternion targetDirection)
    {
        float landingDistance = 15f;
        float landingHeight = 10f;

        Vector3 landingVector = new(0, landingHeight, landingDistance);
        Vector3 rotatedLandingVector = targetDirection * landingVector;
        Debug.Log("Rotated landing vector: " + rotatedLandingVector);
        Vector3 landingStartPoint = targetPosition + rotatedLandingVector;

        GameObject tempGameObject = new("LandingPoint");
        tempGameObject.transform.SetPositionAndRotation(landingStartPoint, targetDirection);
        return tempGameObject.transform;
    }
        
    void FlyToWaypoint(Vector3 targetPosition)
    {
        Debug.Log("Flying to waypoint");
        ShouldBeFlying();
        Vector3 velocity = (targetPosition - transform.position).normalized * maxSpeed;
        Debug.Log("Setting velocity to " + velocity);
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = velocity;
        transform.LookAt(targetPosition);
        while (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            // Do nothing
        }
    }

    void LandAtTarget(Vector3 targetPosition)
    {
        Debug.Log("Initiating landing sequence");
        ShouldBeFlying();
        LandOnWaterAnim();
        FlyToWaypoint(targetPosition);
        SetVelocity(Vector3.zero);
        duckState = DuckState.Swimming;
        // GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("now Swimming");
        OrientHorizontal();
    }

    private void OrientHorizontal()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.x = 0f;
        currentRotation.z = 0f;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void ShouldBeFlying()
    {
        if (DuckState.Flying != duckState)
        {
            throw new System.Exception("Duck is not flying");
        }
    }

    void TurnRight()
    {
        // Calculate the turning force
        Vector3 turningForce = transform.right * maxForce;

        // Apply the turning force to the duck
        GetComponent<Rigidbody>().AddForce(turningForce, ForceMode.Acceleration);
    }

    IEnumerator AnimateDuck()
    {
        // Start with flying animation
        FlyFromWaterAnim();
        yield return new WaitForSeconds(5f); // Time for flying animation to complete

        // Transition to landing animation
        LandOnWaterAnim();
        yield return new WaitForSeconds(5f); // Time for landing animation to complete

        // Transition to flying animation again
        FlyFromWaterAnim();
        yield return new WaitForSeconds(5f); // Time for flying animation to complete

        // Transition to has died in air animation
        HasDiedInAirAnim();
        yield return new WaitForSeconds(5f); // Time for has died in air animation to complete

        Destroy(gameObject);
    }
}
