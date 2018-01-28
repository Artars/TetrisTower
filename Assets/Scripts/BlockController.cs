using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    [Header("Variaveis de mover blocos")]
    [HideInInspector]
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
    public int maxHorizontalDistance = 7;
    private int currentHorizontalDistance = 0;
	private Transform controlledPiece;
    public bool useJoycon;
    public float controllerDeadZone = 0.1f;
    public float downSpeedUp = 2f;
    public Vector2 safeZoneDimension = new Vector2(8, 4);
    private bool tryingToSpawn = false;

    [Header("Veriaveis de usar power ups")]
    public float moveSpeed = 10f;
    public GameObject cursorPrefab;
    public float timeToShoot = 5f;
    public float projectileSpeed = 10f;
    public GameObject popUpPrefab;
    private float shootCounter = 0;
    private bool isUsingPowerUp = false;
    private int debbugNumber = 0;
    private Transform cursor;
    private PowerUpInventory inventory;
    private int powerTypeToShoot;
    private CameraFollow camera;

	// Use this for initialization
	void Start () {
        //piecesPrefab = new GameObject[1];
        //piecesPrefab[0] = GameObject.Find("Q");
        //piecesPrefab[0].transform.position = spawnPlace.position;
        //controlledPiece = piecesPrefab[0].transform;

        //controlledPiece = spawnBlock().transform;
        cursor = GameObject.Instantiate(cursor).transform;
        inventory = GetComponent<PowerUpInventory>();
        camera = GameManager.instance.cameras[player - 1];
        cursor.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        
        //Look for controller
        checkController();

        if (!isUsingPowerUp)
            blockUpdate();
        else
            powerUpUpdate();

        
        if (Input.GetButtonDown(playerString + "Choose")) {
            if (inventory.powerUpCount() > 0)
            {
                cursor.gameObject.SetActive(true);
                isUsingPowerUp = true;
                shootCounter = timeToShoot;
                powerTypeToShoot = inventory.popPowerUp();
                controlledPiece.gameObject.SetActive(false);
                camera.beginFollowTransform(cursor);
            }
        }
        
		
	}

    private void blockUpdate()
    {
        if (controlledPiece != null)
        {
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
            else if (Mathf.Abs(Input.GetAxisRaw(playerString + "Horizontal")) <= controllerDeadZone)
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
        else if (tryingToSpawn)
        {
            bool result = checkForSafeZone();
            if (result)
            {
                tryingToSpawn = false;
                spawnBlock();
            }
        }
    }

    public void stopHoldingBlock() {
        tryingToSpawn = true;
        controlledPiece = null;
    }

    private void powerUpUpdate()
    {
        if (Mathf.Abs(Input.GetAxisRaw(playerString + "Horizontal")) > controllerDeadZone || Mathf.Abs(Input.GetAxisRaw(playerString + "Vertical")) > controllerDeadZone)
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw(playerString + "Horizontal"), Input.GetAxisRaw(playerString + "Vertical"), 0);
            cursor.position += direction * moveSpeed * Time.deltaTime;
        }
        else if (Mathf.Abs(Input.GetAxisRaw(playerString + "Horizontal")) <= controllerDeadZone)
        {
            holdTimer = 0;
        }
        if (holdTimer <= 0 || Input.GetButtonDown(playerString + "Fire")) {
            shootAtPoint(cursor.position, PowerUp.Type.Acid);
        }
        holdTimer -= Time.deltaTime;
    }

    private void shootAtPoint(Vector3 target, PowerUp.Type pType) {
        GameObject prefab = inventory.powerUpsPrefabs[powerTypeToShoot];
        GameObject bullet = GameObject.Instantiate(prefab, spawnPlace.position, Quaternion.identity);
        MoveForwardsAngle mfa = bullet.GetComponent<MoveForwardsAngle>();
        Vector3 direction = target - spawnPlace.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        mfa.angle = angle;

        GameObject popUp = GameObject.Instantiate(popUpPrefab);
        CameraPopUp pS = popUp.GetComponent<CameraPopUp>();
        pS.followTarget(bullet.transform, player, GameManager.instance.getMaxPlayers());

        //Volta a mover bloco
        isUsingPowerUp = false;
        controlledPiece.gameObject.SetActive(true);
        cursor.gameObject.SetActive(false);
        camera.returnToFollowLine();
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
        if (Mathf.Abs(currentHorizontalDistance + clamped) < maxHorizontalDistance - 1)
        {
            Vector3 direction = new Vector3(clamped * snapPosition, 0);
            controlledPiece.position += direction;
            currentHorizontalDistance += clamped;
        }
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
        GameObject spawnedBlock = GameObject.Instantiate(GameManager.instance.spawnController.getNextBlock(player-1));
        Block blockScript = spawnedBlock.GetComponent<Block>();
        blockScript.setController(this);
        controlledPiece = spawnedBlock.transform;
        spawnedBlock.name = "Player " + player + "Number: " + debbugNumber;
        debbugNumber++;
        if (blockScript.shape == Block.Shape.I || blockScript.shape == Block.Shape.O)
            controlledPiece.position = spawnPlace.position;
        else
            controlledPiece.position = spawnPlace.position + new Vector3(0.5f, 0.5f);
        currentHorizontalDistance = 0;
        //return GameObject.Instantiate(piecesPrefab[randomPrefab], spawnPlace.position, Quaternion.identity);
    }

    private bool checkForSafeZone()
    {
        Collider2D[] hits;
        hits = Physics2D.OverlapBoxAll(spawnPlace.position, safeZoneDimension, 0);
        bool hasFound = false;

        if (hits.Length > 0) {
            foreach (Collider2D hit in hits) {
                if (hit == null)
                    break;
                Transform parent = hit.transform.parent;
                if (parent != null && parent.gameObject.tag.Equals("Block")) {
                    hasFound = true;
                    break;
                }
            }
        }

        return !hasFound;
    }

}
