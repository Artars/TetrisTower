using UnityEngine;
using System.Collections;

/*


 */
public class Block : MonoBehaviour{

    public enum Type{Wood, Glass, Metal, Rubber};
    public enum Shape{I,J,L,O,S,Z,T};

    public bool controlable;

    private bool gettingDeleted;
    public Type type;
    public Shape shape;
    public Rigidbody2D rb;
    private BlockController controller;
    public int player = -1;

    void Start()
    {
        controlable = true;
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
            //rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        //rb.isKinematic = true;
        gettingDeleted = false;
    }

    public void setController(BlockController bc) {
        controller = bc;
        player = controller.player;
    }

    /*
        Tocar em outro bloco
     */
    void OnCollisionEnter2D(Collision2D other)
    {
        print("Colide: " + other.gameObject.name);
        if(controlable)
        {
            if(other.gameObject.tag == "Block" ||  other.gameObject.tag == "Ground")
            {
                controlable = false;
                rb.gravityScale = 1;
                if(controller != null)
                    controller.stopHoldingBlock();
            }
        }
        else
        {
            // testa se é algum power
            switch(other.gameObject.tag) 
            {
                case "power_fire":
                    if(type == Type.Wood || type == Type.Rubber)
                    {
                        cascateDeletion();
                    }
                    break;
                case "power_rock": 
                    if(type == Type.Glass || type == Type.Wood)
                    {
                        cascateDeletion();
                    }
                    break;
                case "power_acid":
                    if(type == Type.Metal || type == Type.Rubber)
                    {
                        cascateDeletion();
                    }
                    break;

            }
        }
    }

    public bool isControlable()
    {
        return controlable;
    }

    private void cascateDeletion()
    {
        // TODO: the animation
        Block[] blocks = Object.FindObjectsOfType(typeof(Block)) as Block[];
        gettingDeleted = true;

        foreach (Block block in blocks)
        {
            if(block.gameObject == gameObject)
            {
                continue;
            }

            if(block.GetComponent<Collider2D>().IsTouching(GetComponent<Collider2D>()))
            {
                if(block.type == type)
                {
                    if(!block.gettingDeleted)
                    {
                        block.cascateDeletion();
                    }
                }
            }
        }

        Destroy(gameObject);
    }
}