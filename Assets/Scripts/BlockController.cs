using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    public Transform spawnPlace;
	public float waitButtonHoldTime = 0.5f;
    private float holdTimer = 0f;
    public float snapPosition = 1f;
    public float verticalSpeed= 3f;
    public float rotationAngle = 45f;
    public int player = 1;
    private string playerString;
    private string keyboardString;
    private string joystickString;
	public GameObject[] piecesPrefab;
	private Transform controlledPiece;
    public bool useJoycon;
    public float controllerDeadZone = 0.1f;
    public float downSpeedUp = 2f;
         
	// Use this for initialization
	void Start () {
        //piecesPrefab = new GameObject[1];
        //piecesPrefab[0] = GameObject.Find("Q");
        //piecesPrefab[0].transform.position = spawnPlace.position;
        //controlledPiece = piecesPrefab[0].transform;

        //controlledPiece = spawnBlock().transform;

	}
	
	// Update is called once per frame
	void Update () {
		if(controlledPiece != null) {
            //Look for controller
            checkController();

            //Horizontal input
            if (Input.GetButtonDown(playerString + "Horizontal"))
            {
                moveBlock(Input.GetAxisRaw(playerString + "Horizontal"));
                holdTimer = waitButtonHoldTime;
            }
            else if (Mathf.Abs(Input.GetAxisRaw(playerString + "Horizontal")) > controllerDeadZone && holdTimer <= 0)
            {
                moveBlock(Input.GetAxis(playerString + "Horizontal"));
                holdTimer = waitButtonHoldTime;
            }
            else if(Mathf.Abs(Input.GetAxisRaw(playerString + "Horizontal")) <= controllerDeadZone)
            {
                holdTimer = 0;
            }

            //Rotate input
            if (Input.GetButtonDown(playerString + "Rotate"))
            {
                rotateBlock(Input.GetAxis(playerString + "Rotate"));
            }

            //Down input
            if (Input.GetAxisRaw(playerString + "Vertical") < -controllerDeadZone)
            {
                controlledPiece.position += new Vector3(0, -verticalSpeed * Time.deltaTime * downSpeedUp);
            }
            else
            {
                controlledPiece.position += new Vector3(0, -verticalSpeed * Time.deltaTime);
            }
            holdTimer -= Time.deltaTime;
		}
		
	}

    public void stopHoldingBlock() {
        spawnBlock();
    }

    public void setPlayer(int player) {
        this.player = player;
        joystickString = playerString = "P" + player + "_Joy_";
        keyboardString = playerString = "P" + player + "_";
        if (useJoycon)
            playerString = "P" + player + "_Joy_";
        else
            playerString = "P" + player + "_";
    }

    public void startGame() {
        spawnBlock();
    }

    private void checkController() {
        if(Input.GetButtonDown(keyboardString + "Horizontal"))
        {
            useJoycon = false;
            playerString = keyboardString;
        }
        else if (Input.GetAxisRaw(joystickString + "Horizontal") != 0)
        {
            useJoycon = true;
            playerString = joystickString;
        }
    }



    /// <summary>
    /// Move bloco de acordo com o grid
    /// </summary>
    /// <param name="way">Eixo de movimentos</param>
	private void moveBlock(float axis) {
        int clamped = (axis > 0) ? 1 : -1;
        Vector3 direction = new Vector3(clamped * snapPosition, 0);
        controlledPiece.position += direction;
    }

    /// <summary>
    /// Rotaciona o bloco em 90º
    /// </summary>
    /// <param name="axis">Eixo de movimento</param>
    private void rotateBlock(float axis)
    {
        int clamped = (axis > 0) ? 1 : -1;
        Quaternion rotation = controlledPiece.localRotation;
        rotation = Quaternion.AngleAxis(rotationAngle, controlledPiece.forward) * controlledPiece.rotation;
        controlledPiece.localRotation = rotation;
    }

    private void spawnBlock() {
        GameObject spawnedBlock = GameManager.instance.spawnController.getNextBlock(player-1, spawnPlace.position, Quaternion.identity);
        Block blockScript = spawnedBlock.GetComponent<Block>();
        blockScript.setController(this);
        controlledPiece = spawnedBlock.transform;
        if (blockScript.shape == Block.Shape.I || blockScript.shape == Block.Shape.O)
            controlledPiece.position += new Vector3(0.5f, 0);
        //return GameObject.Instantiate(piecesPrefab[randomPrefab], spawnPlace.position, Quaternion.identity);
    }

}
