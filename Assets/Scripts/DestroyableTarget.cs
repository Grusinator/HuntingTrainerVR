
using UnityEngine;

public class DestroyableTarget : MonoBehaviour
{
    [SerializeField] private GameObject destroyedPrefab;


    public KeepTrackOfTargetsHit targetStatistics;

    void Start()
    {
        targetStatistics = GameObject.FindObjectOfType<KeepTrackOfTargetsHit>();
    }
    public void DestroyTarget()
    {
        Instantiate(destroyedPrefab, transform.position, transform.rotation);
        Debug.Log("Destroying " + gameObject.name + " position: " + transform.position + " rotation: " + transform.rotation + " destroyedPrefab: " + destroyedPrefab.name);
        targetStatistics.TargetHit();
        Destroy(gameObject);
    }
}