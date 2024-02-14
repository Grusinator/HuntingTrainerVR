using System.Collections;
using System.Threading.Tasks;
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
            Vector3 randomPosition = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));
            GameObject duck = Instantiate(duckPrefab, spawnPosition.transform.position + randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Duck duckMovement = duck.GetComponent<Duck>();
            Task.Delay(System.TimeSpan.FromSeconds(2));
            duckMovement.navAgent.SetDestination(waterTransform.position);
        }
    }
}
