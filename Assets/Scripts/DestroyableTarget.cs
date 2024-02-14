
using UnityEngine;

public class DestroyableTarget : DestroyableObject
{
    [SerializeField] private GameObject destroyedPrefab;

    public void DestroyObject()
    {
        Instantiate(destroyedPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}





