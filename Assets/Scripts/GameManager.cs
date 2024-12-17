using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    [Header("Game Variables")]
    public int startingLives;
    public int lives;
    public TextMeshProUGUI livesDisplay;


    // Start is called before the first frame update
    void Start()
    {
        sceneSpeed = startingSceneSpeed;
        UpdateLives();
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

    IEnumerator UglyRestart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateLives()
    {
        livesDisplay.text = $"Lives: {lives}";
    }

    public void LoseLife()
    {
        lives--;
        UpdateLives();

        StopScene();

        if(lives > 0)
        {
            StartCoroutine(UglyRestart());
        }

    }
}
