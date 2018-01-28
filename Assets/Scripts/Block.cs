using UnityEngine;
using System.Collections;

/*


 */
public class Block : MonoBehaviour{

    public enum Type{Wood, Glass, Metal, Rock};
    public enum Shape{I,J,L,O,S,Z,T};

<<<<<<< Updated upstream
    public bool controlable;
=======
    private bool deleting = false;

    private bool controlable;
>>>>>>> Stashed changes

    private bool gettingDeleted;
    public Type type;
    public Shape shape;
    public Rigidbody2D rb;
    private BlockController controller;
    public int player = -1;

    private float deletionTimer = 0f;
    private static readonly float deletionDuration = 1f;
    void Start()
    {
        controlable = true;
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
            //rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        //rb.isKinematic = true;
        gettingDeleted = false;
    }

    void Update()
    {
        if(deleting)
        {
            deletionTimer += Time.deltaTime;
            if(deletionTimer > deletionDuration )
            {
                Destroy(gameObject)
            }
        }
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
                    if(type == Type.Wood)
                    {
                        cascateDeletion(PowerUp.Type.Fire);
                    }
                    break;
                case "power_rock": 
                    if(type == Type.Glass || type == Type.Wood)
                    {
                        cascateDeletion(PowerUp.Type.Rock);
                    }
                    break;
                case "power_acid":
                    if(type == Type.Metal)
                    {
                        cascateDeletion(PowerUp.Type.Acid);
                    }
                    break;

            }
        }
    }

    public bool isControlable()
    {
        return controlable;
    }

    private void cascateDeletion(PowerUp.Type damageType)
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

                        deleting = true;
                        block.cascateDeletion(damageType);
                    }
                }
            }
        }
    }
}