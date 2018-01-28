using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Vector3 initialPosition;
    public float minPorcentage = 0f;
    public float maxPorcentage = 0.6f;
    public float verticalSpeed = 1f;
    private int player;
    private Camera myCamera;
    private Vector3 minOffset;
    private Vector3 maxOffset;
    private Transform myTransform;
    private Collider2D[] buffer;
    private Vector3 cameraSpeed;
    public Vector2 collisionCheckBox = new Vector2(8, 1);

    //Follow variables
    private Vector3 lastPosition;
    private Transform targetToFollow;
    private bool isFollowing = false;
    public float smoothFollowConstant = 0.3f;
    public float maxFollowSpeed = 20f;
    public float timeToReturn = 0.5f;
    private Vector3 followOffset;

    public void setPlayer(int player, int maxPlayers) {
        float ratio = 1f / maxPlayers;
        myCamera.rect = new Rect((player - 1) * ratio, 0, ratio, 1);
        this.player = player;
    }

	// Use this for initialization
	void Awake () {
        myCamera = GetComponent<Camera>();
        myTransform = transform;
        initialPosition = myTransform.position;
        Vector3 offset = myTransform.GetChild(0).position - myTransform.position;
        minOffset = new Vector3(0, offset.y * (minPorcentage - 1f));
        maxOffset = new Vector3(0, offset.y * (maxPorcentage - 1f));
        buffer = new Collider2D[30];
        cameraSpeed = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isFollowing)
        {

            int count = Physics2D.OverlapBoxNonAlloc(myTransform.position + minOffset, collisionCheckBox, 0, buffer);
            bool detected = false;
            buffer[count] = null;
            //Verifica se há blocos ou o chão no inferior da tela
            if (count > 0)
            {
                foreach (Collider2D col in buffer)
                {
                    if (col == null)
                        break;
                    Transform parent = col.transform.parent;

                    if (parent != null && parent.gameObject.tag.Equals("Block"))
                    {
                        Block blockScript = parent.GetComponent<Block>();
                        if (!blockScript.controlable)
                        {
                            detected = false;
                            break;
                        }
                        else
                        {
                            cameraSpeed = new Vector3(0, -verticalSpeed);
                            detected = true;
                        }
                    }
                    else
                    {
                        cameraSpeed = new Vector3(0, -verticalSpeed);
                        detected = true;
                    }
                }
            }
            else
            {
                cameraSpeed = new Vector3(0, -verticalSpeed);
                detected = true;
            }

            count = Physics2D.OverlapBoxNonAlloc(myTransform.position + maxOffset, collisionCheckBox, 0, buffer);
            buffer[count] = null;
            if (count > 0)
            {
                foreach (Collider2D col in buffer)
                {
                    if (col == null)
                        break;
                    Transform parent = col.transform.parent;
                    if (parent != null && parent.gameObject.tag.Equals("Block"))
                    {
                        Block blockScript = parent.GetComponent<Block>();
                        if (!blockScript.controlable)
                        {
                            cameraSpeed = new Vector3(0, verticalSpeed);
                            detected = true;
                            break;
                        }
                    }
                }
            }

            if (!detected)
            {
                cameraSpeed = Vector3.zero;
            }

            myTransform.position += cameraSpeed * Time.deltaTime;
            if (myTransform.position.y < initialPosition.y)
                myTransform.position = initialPosition;
        }

        //Comportamento de seguir alvo
        else {
            myTransform.position = Vector3.SmoothDamp(myTransform.position, targetToFollow.position - followOffset,
                ref cameraSpeed, smoothFollowConstant, maxFollowSpeed);
        }

    }

    public void beginFollowTransform(Transform target) {
        lastPosition = myTransform.position;
        cameraSpeed = Vector3.zero;
        isFollowing = true;
        targetToFollow = target;
        followOffset = new Vector3(0,0,(target.position - myTransform.position).z);
    }

    public void returnToFollowLine() {
        //Chama função para retorna a camara para a posição
        targetToFollow = new GameObject("Retorna").transform;
        targetToFollow.position = lastPosition;
        followOffset = Vector3.zero;
        StartCoroutine(waitToCatchUp());
    }

    private IEnumerator waitToCatchUp() {
        float delta = timeToReturn;
        while (delta > 0) {
            delta -= Time.deltaTime;
            yield return null;
        }

        isFollowing = false;
    }

    /*
    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(collisionCheckBox.x, collisionCheckBox.y, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(myTransform.position + maxOffset, size);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(myTransform.position + minOffset, size);
    }
    */
}
