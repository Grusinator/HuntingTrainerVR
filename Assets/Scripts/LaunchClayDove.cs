using System.Collections;
using UnityEngine;

public class LaunchClayDove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The projectile that's created")]
    GameObject m_ProjectilePrefab = null;

    [SerializeField]
    [Tooltip("The point that the projectile is created")]
    Transform m_StartPoint = null;

    [SerializeField]
    [Tooltip("The speed at which the projectile is launched")]
    public float m_LaunchSpeed = 5f;

    [SerializeField]
    [Tooltip("The standard deviation of the forward angle")]
    float m_ForwardAngleDeviation = 10.0f;

    [SerializeField]
    [Tooltip("The time delay before launching the dove")]
    float m_TimeDelay = 0.0f;

    [SerializeField]
    [Tooltip("The angle of inclination for the projectile")]
    float m_InclinationAngle = 0.0f;

    private KeepTrackOfTargetsHit targetStatistics;

    void Start()
    {
        targetStatistics = GameObject.FindObjectOfType<KeepTrackOfTargetsHit>();
    }


    public void Fire()
    {
        StartCoroutine(FireWithDelay());
    }

    private IEnumerator FireWithDelay()
    {
        yield return new WaitForSeconds(m_TimeDelay);
        GameObject clayDove = InstantiateClayDove();
        ApplyForce(clayDove);
        targetStatistics.TargetLaunched();

    }

    private GameObject InstantiateClayDove()
    {
        float randomXRotation = Random.Range(-m_ForwardAngleDeviation, m_ForwardAngleDeviation);
        float randomYRotation = Random.Range(-m_ForwardAngleDeviation, m_ForwardAngleDeviation);
        Quaternion adjustedRotation = Quaternion.Euler(m_InclinationAngle, randomYRotation, randomXRotation);
        GameObject newObject = Instantiate(m_ProjectilePrefab, m_StartPoint.position, m_StartPoint.rotation * adjustedRotation, null);
        return newObject;
    }

    void ApplyForce(GameObject clayDove)
    {
        if (clayDove.TryGetComponent(out Rigidbody rigidBody))
        {
            rigidBody.velocity = clayDove.transform.forward * m_LaunchSpeed;
        }
        
    }
}

