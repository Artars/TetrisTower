using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Transform[] spawnPlace;
    public GameObject controllerPrefab;
    private List<BlockController> playerControllers;
    public SpawnController spawnController;
    public GameObject nuvem;
    
    private void Awake()
    {
        //Singleton constrution
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this) {
            Destroy(gameObject);
        }

        GetComponent<SpawnController>();
    }

    private void Start()
    {
        setupScene();
        startGame();
    }


    public void setupScene()
    {
        playerControllers = new List<BlockController>();
        for (int i = 0; i < spawnPlace.Length; i++) {
            GameObject spawnedController = GameObject.Instantiate(controllerPrefab);
            BlockController controller = spawnedController.GetComponent<BlockController>();
            controller.setPlayer(i + 1);
            playerControllers.Add(controller);
            controller.spawnPlace = spawnPlace[i];
        }
    }

    public void startGame()
    {
        foreach (BlockController bc in playerControllers)
        {
            bc.startGame();
        }
        
        //Instancia as nuvens
        for (int x = 0; x < 10; x++)
            Instantiate(nuvem, new Vector3(Random.Range(-20, 100.0f), Random.Range(3, 15.0f), 0), nuvem.transform.rotation);
    }
}