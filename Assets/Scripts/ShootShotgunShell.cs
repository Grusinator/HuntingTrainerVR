using UnityEngine;

public class ShootShotgunShell : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The projectile that's created")]
    GameObject m_ProjectilePrefab = null;

    [SerializeField]
    [Tooltip("The point that the project is created")]
    Transform m_StartPoint = null;

    [SerializeField]
    [Tooltip("The speed at which the projectile is launched")]
    public float m_LaunchSpeed = 400f;

    [SerializeField]
    [Tooltip("The radius within which the pellets are spawned")]
    float m_SpawnRadius = 1.0f;

    [SerializeField]
    [Tooltip("The number of pellets to spawn")]
    int m_NumPellets = 10;

    [SerializeField]
    [Tooltip("The standard deviation of the forward angle")]
    float m_ForwardAngleDeviation = 10.0f;

    [SerializeField]
    [Tooltip("The guiding object prefab")]
    GameObject m_GuidingObjectPrefab = null;

    [SerializeField]
    [Tooltip("The sound played when the gun is fired")]
    private AudioClip m_FireSound;

    [SerializeField]
    [Tooltip("The particle system prefab")]
    private GameObject m_SmokeParticleSystemPrefab;

    public bool m_guide_enabled = true;

    private KeepTrackOfTargetsHit targetStatistics;

    void Start()
    {
        targetStatistics = GameObject.FindObjectOfType<KeepTrackOfTargetsHit>();
    }

    public void Fire()
    {
        PlaySoundAndSmoke();
        SpawnHailShot();
        AddGuideObject();
        targetStatistics.ShotFired();
    }

    private void PlaySoundAndSmoke()
    {
        // Play the fire sound
        AudioSource.PlayClipAtPoint(m_FireSound, m_StartPoint.position);

        ParticleSystem m_SmokeParticleSystem = Instantiate(m_SmokeParticleSystemPrefab, m_StartPoint).GetComponent<ParticleSystem>();
        m_SmokeParticleSystem.Play();
    }


    private void SpawnHailShot()
    {
        // Spawn the shotgun shells
        for (int i = 0; i < m_NumPellets; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * m_SpawnRadius;
            float xRotation = Random.Range(-m_ForwardAngleDeviation, m_ForwardAngleDeviation);
            float yRotation = Random.Range(-m_ForwardAngleDeviation, m_ForwardAngleDeviation);
            Quaternion randomRotation = Quaternion.Euler(xRotation, yRotation, 0);
            Vector3 spawnPosition = m_StartPoint.position + randomOffset;
            GameObject newObject = Instantiate(m_ProjectilePrefab, spawnPosition, m_StartPoint.rotation * randomRotation, null);

            if (newObject.TryGetComponent(out Rigidbody rigidBody))
            {
                rigidBody.velocity = newObject.transform.forward * m_LaunchSpeed;
            }
        }
    }


    private void AddGuideObject()
    {
        if (!m_guide_enabled)
        {
            Debug.Log("Guide object not enabled");
            return;
        }
        // Spawn the guiding object
        GameObject guidingObject = Instantiate(m_GuidingObjectPrefab, m_StartPoint.position, m_StartPoint.rotation, null);
        guidingObject.transform.localScale = new Vector3(m_SpawnRadius, m_SpawnRadius, m_SpawnRadius);
        guidingObject.GetComponent<ScaleWithDistance>().expansionInDegrees = m_ForwardAngleDeviation;
        if (guidingObject.TryGetComponent(out Rigidbody guideRigidBody))
        {
            guideRigidBody.velocity = guideRigidBody.transform.forward * m_LaunchSpeed;
        }
    }

}
