using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPopUp : MonoBehaviour {

    public float smoothTime = 0.3f;
    public float maxSpeed = 10f;
    private Vector3 speed;
    private Camera myCamera;
    private Transform target;
    private int player;
    private Transform myTransform;
    private Vector3 offset;

	// Use this for initialization
	void Awake () {
        myCamera = GetComponent<Camera>();
        myCamera.rect = new Rect(0, 0, 0, 0);
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            myTransform.position = Vector3.SmoothDamp(myTransform.position, target.position - offset, ref speed, smoothTime, maxSpeed);
        }
        else
            Destroy(gameObject);
	}


    /// <summary>
    /// Começa a perseguir um dado alvo
    /// </summary>
    /// <param name="target">Alvo a ser perseguido</param>
    /// <param name="player">Player que criou essa tela</param>
    /// <param name="maxPlayers">Numero máximo de jogadores na fase</param>
    public void followTarget(Transform target, int player, int maxPlayers) {
        this.target = target;
        this.player = player;
        speed = Vector3.zero;

        offset = new Vector3(0, 0, (target.position - myTransform.position).z);

        float ratio = 1f / maxPlayers;

        Rect rectToPlace;

        if (player != maxPlayers)
        {
            rectToPlace = new Rect(player * 0.75f * ratio, 0.7f, ratio * 0.5f, 0.2f);
        }
        else {
            rectToPlace = new Rect((player-1) * 0.75f * ratio, 0.7f, ratio * 0.5f, 0.2f);
        }
        print("Rect: " + rectToPlace);
        myCamera.rect = rectToPlace;
        print("Camera Rect: " + myCamera.rect);
    }
}
