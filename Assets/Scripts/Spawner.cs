using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPrefabs;
    private GameManager gameManager;
    private float horizontalMin;
    private float horizontalMax;
    [SerializeField] private float verticalPos;
    [SerializeField] private float baseDelay;
    [SerializeField] private float delayVariation;

    [SerializeField] private int maxSpawns;

    public bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        horizontalMin = gameManager.horizontalMin;
        horizontalMax = gameManager.horizontalMax;

        transform.position = new Vector3(0, verticalPos, gameManager.forwardMax);

        StartCoroutine(TimedSpawn());
    }

    public Vector3 RandomPosition()
    {
        return new Vector3(
            Random.Range(horizontalMin, horizontalMax),
            transform.position.y,
            transform.position.z
            );
    }

    public void SpawnObstacle(int numberToSpawn)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {

            Instantiate(
                spawnPrefabs[Random.Range(0, spawnPrefabs.Length)],
                RandomPosition(),
                transform.rotation
                );

        }
    }

    IEnumerator TimedSpawn()
    {
        if (isSpawning)
        {
            SpawnObstacle(Random.Range(0, maxSpawns));
            yield return new WaitForSeconds(baseDelay + Random.Range(-delayVariation, delayVariation));
            if (gameManager.isGameActive)
            {
                StartCoroutine(TimedSpawn());
            }
        }
    }


}
