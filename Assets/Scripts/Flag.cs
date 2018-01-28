using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour {

    public GameObject canvasPrefab;

    private void OnTriggerStay2D(Collider2D col)
    {
        //print("Colidiu " + col.gameObject.name);
        Transform parent = col.transform.parent;
        if (parent != null && parent.gameObject.tag.Equals("Block")) {
            Block blockScript = parent.GetComponent<Block>();
            if (!blockScript.controlable)
            {
                //print("Jogador " + blockScript.player + " atingiu o alvo");
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.color = new Color(1, 1, 1, 0.2f);

                GameObject canvas = GameObject.Instantiate(canvasPrefab);
                Text text = canvas.GetComponentInChildren<Text>();

                text.text = "Congratulations!\nPlayer " + blockScript.player + " won!";
            }
        }
    }
}
