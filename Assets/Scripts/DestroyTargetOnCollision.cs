using Unity.VisualScripting;
using UnityEngine;

public class DestroyTargetOnCollision : MonoBehaviour
{
    public GameObject bulletHolePrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Projectile"))
        {
            return;
        }

        // Spawn bullet hole mark
        var bulletHole = Instantiate(bulletHolePrefab, collision.contacts[0].point, Quaternion.LookRotation(gameObject.transform.forward));
        bulletHole.transform.parent = collision.gameObject.transform;

        // Destroy bullet
        Destroy(gameObject);

        if (collision.gameObject.TryGetComponent<DestroyableTarget>(out var destroyableTarget))
        {
            Debug.Log("Destroying " + collision.gameObject.name + " position: " + collision.gameObject.transform.position + " rotation: " + collision.gameObject.transform.rotation);
            destroyableTarget.DestroyObjectAndTrack();
        }

        if (collision.gameObject.TryGetComponent<lb_Bird>(out var bird))
        {
            Debug.Log("Destroying " + collision.gameObject.name + " position: " + collision.gameObject.transform.position + " rotation: " + collision.gameObject.transform.rotation);
            bird.KillBird();
        }

        if (collision.gameObject.TryGetComponent<Duck>(out var duck))
        {
            Debug.Log("Destroying " + collision.gameObject.name + " position: " + collision.gameObject.transform.position + " rotation: " + collision.gameObject.transform.rotation);
            duck.DestroyObjectAndTrack();
        }
    }
}