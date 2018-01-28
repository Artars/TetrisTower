using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Transform[] spawnPlace;
    public GameObject controllerPrefab;
    private List<BlockController> playerControllers;
    public CameraFollow[] cameras;
    public SpawnController spawnController;
    public GameObject nuvem;

    //DEBUG
    public GameObject popUpCameraPreafab;
    private Transform targetToFollow;

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
        for (int i = 0; i < cameras.Length; i++) {
            CameraFollow camScript = cameras[i].GetComponent<CameraFollow>();
            camScript.setPlayer(i + 1, spawnPlace.Length);
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

    public int getMaxPlayers() {
        return spawnPlace.Length;
    }

    private void Update()
    {
        if (popUpCameraPreafab != null)
        {
            if (Input.GetButtonDown("P1_Rotate"))
            {
                targetToFollow = new GameObject("Target").transform;
                GameObject cam = GameObject.Instantiate(popUpCameraPreafab);
                cam.GetComponent<CameraPopUp>().followTarget(targetToFollow, 1, getMaxPlayers());
            }
            else if (Input.GetButtonUp("P1_Rotate"))
            {
                Destroy(targetToFollow.gameObject);
            }
            if (Input.GetButton("P1_Rotate"))
            {
                Camera cam = cameras[0].GetComponent<Camera>();
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = targetToFollow.position.z;
                targetToFollow.position = mousePos;
            }
        }
    }
}