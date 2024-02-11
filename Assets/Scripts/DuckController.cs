using System.Collections;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    public GameObject duckPrefab;
    public float spawnRadius = 200f;
    public Transform waterTransform;

    public Transform spawnPosition;

    private void Start()
    {
        StartCoroutine(SpawnDucks());
    }

    private IEnumerator SpawnDucks()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            Debug.Log("Spawning duck");
            GameObject duck = Instantiate(duckPrefab, spawnPosition.transform.position, Quaternion.identity);
            Duck duckMovement = duck.GetComponent<Duck>();
            duckMovement.LandOnWater(waterTransform.position);
        }
    }
}
