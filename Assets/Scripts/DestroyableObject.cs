

using UnityEngine;

public abstract class DestroyableObject : MonoBehaviour
{
    public KeepTrackOfTargetsHit targetStatistics;

    void Start()
    {
        targetStatistics = FindObjectOfType<KeepTrackOfTargetsHit>(includeInactive: true);
    }

    public void DestroyObjectAndTrack()
    {
        Debug.Log("Destroying " + gameObject.name + " position: " + transform.position + " rotation: " + transform.rotation);
        if (targetStatistics == null)
        {
            Debug.LogError("No target statistics found");
        }
        else
        {
            Debug.Log("Target statistics found");
            targetStatistics.TargetHit();
        }
        
        DestroyObject();
    }

    // This method should either be virtual with a default implementation or just a regular method
    // Since abstract methods in MonoBehaviour derived classes are not supported directly.
    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }
}