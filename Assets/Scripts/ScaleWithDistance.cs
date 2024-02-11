using UnityEngine;

public class ScaleWithDistance : MonoBehaviour
{
    [SerializeField]
    [Tooltip("expansion angle measured from the forward axiz in degrees")]
    public float expansionInDegrees = 4f; // How much the object scales per unit of distance traveled



    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(lastPosition, transform.position);
        float expansionFactor = Mathf.Sin(Mathf.Deg2Rad * expansionInDegrees);
        transform.localScale += new Vector3(1, 1, 1) * (distanceMoved * expansionFactor);
        lastPosition = transform.position;
    }
}
