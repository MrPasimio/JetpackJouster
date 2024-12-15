using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scene Limits")]
    public float horizontalMin;
    public float horizontalMax;
    public float verticalMin;
    public float verticalMax;
    public float forwardMin;
    public float forwardMax;

    [SerializeField] private float startingSceneSpeed;
    public float sceneSpeed;

    public bool isGameActive;




    // Start is called before the first frame update
    void Start()
    {
        sceneSpeed = startingSceneSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopScene()
    {
        //Stop the Spawners
        Spawner[] spawners = GameObject.FindObjectsOfType<Spawner>();   
        foreach(Spawner spawner in spawners)
        {
            spawner.isSpawning = false;
        }

        //Stop the moving items
        sceneSpeed = 0;
        MoveForward[] movers = GameObject.FindObjectsOfType<MoveForward>();
        foreach(MoveForward mover in movers)
        {
            mover.UpdateSpeed();
        }
        
    }
}
