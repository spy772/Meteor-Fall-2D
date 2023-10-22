using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public float spawnRate = 2f;
    [SerializeField] GameObject[] meteorTypes;
    public bool canSpawn = true;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public float minGravity;
    public float maxGravity;
    
    // Start is called before the first frame update    
    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner() {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn) {
            yield return wait;
            int randomMeteor = Random.Range(0, meteorTypes.Length - 1);
            GameObject meteorToSpawn = meteorTypes[randomMeteor];
            Rigidbody2D rigidbody = meteorToSpawn.GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = Random.Range(minGravity, maxGravity);
            Vector3 randomSpawn = generateRandomSpawn();

            Instantiate(meteorToSpawn, randomSpawn, Quaternion.identity);
        }
    }   

    private Vector3 generateRandomSpawn() {
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );

        return randomPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
