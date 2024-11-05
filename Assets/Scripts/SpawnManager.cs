using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject obstaclePF;
    private Vector3 spawnPos = new Vector3 (25, 0, 0);
    private PlayerController playerControllerScript;
    private float startDelay = 2;
    private float repeatRate = 2;

    private float spawnedAmount;
    private float difficultyAmount;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(ObstacleSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        HandleDifficulty();
        
    }

    //Esteen spawnaaminen
    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePF, spawnPos, obstaclePF.transform.rotation);
            spawnedAmount++;
          
        }
           
    }

    //IEnumreator mikä määrittää esteen spawnausnopeuden (repeatRate) sekä ensimmäisen esteen viive (startDelay)
    private IEnumerator ObstacleSpawner()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(repeatRate);
        }
    }

    //switch case mikä muuttaa esteiden spawnaamisnopeutta, riippuen ylitetyistä esteistä
    //levelin muuttuessa muutetaan myös UI näkyvä level teksti vastaavaksi
    void HandleDifficulty()
    {
        difficultyAmount = spawnedAmount / 10;
       

        switch (difficultyAmount)
        {
            case 1:
                repeatRate = 1.25f;
                GameHandler.instance.levelText.text = "Level 2";
                break;

            case 2:
                repeatRate = 0.75f;
                GameHandler.instance.levelText.text = "Level 3";
                break;
            case 3:
                repeatRate = 0.55f;
                GameHandler.instance.levelText.text = "Level 4";
                break;
        }
      
    }
}
