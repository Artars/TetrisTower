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
	public GameObject[] piecesPrefab;
	private Transform controlledPiece;
	
	// Use this for initialization
	void Start () {
        piecesPrefab = new GameObject[1];
        piecesPrefab[0] = GameObject.Find("Q");
        piecesPrefab[0].transform.position = spawnPlace.position;
        controlledPiece = piecesPrefab[0].transform;

        //controlledPiece = spawnBlock().transform;
        playerString = "P" + player + "_";
	}
	
	// Update is called once per frame
	void Update () {
		if(controlledPiece != null) {
            if (Input.GetButtonDown(playerString + "Horizontal"))
            {
                moveBlock(Input.GetAxis(playerString + "Horizontal"));
                holdTimer = waitButtonHoldTime;
            }
            else if (Input.GetButton(playerString + "Horizontal") && holdTimer <= 0)
            {
                moveBlock(Input.GetAxis(playerString + "Horizontal"));
                holdTimer = waitButtonHoldTime;
            }
            else if (Input.GetButtonDown(playerString + "Rotate"))
            {
                rotateBlock(Input.GetAxis(playerString + "Rotate"));
            }
                controlledPiece.position += new Vector3(0, -verticalSpeed * Time.deltaTime);
            holdTimer -= Time.deltaTime;
		}
		
	}

    public void stopHoldingBlock() {
        controlledPiece = spawnBlock().transform;
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

        private GameObject spawnBlock() {
        int randomPrefab = Random.Range(0, piecesPrefab.Length - 1);
        return GameObject.Instantiate(piecesPrefab[randomPrefab], spawnPlace.position, Quaternion.identity);
        //return GameObject.Instantiate(piecesPrefab[randomPrefab], spawnPlace.position, Quaternion.identity);
    }

}
