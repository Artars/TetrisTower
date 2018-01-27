using UnityEngine;
using System.Collections;

/*


 */
public class Block : MonoBehaviour{

    public enum Type{Wood, Glass, Metal, Borracha};
    public enum Shape{T,Z,_Z,I,L,_L};

    private bool controlable;
    public Type type;
    public Shape shape;
    public Rigidbody2D rb;

    void Start()
    {
        controlable = true;
        rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb.isKinematic = true;
    }

    /*
        Tocar em outro bloco
     */
    void OnCollisionEnter(Collision col)
    {
        if(controlable)
        {
            if(col.gameObject.tag == "Block" ||  col.gameObject.tag == "Ground")
            {
                controlable = false;
                rb.isKinematic = false;
            }
        }
        else
        {
            // testa se é algum power
            switch(col.gameObject.tag) 
            {
                case "power_fire":
                    if(type == Type.Wood || type == Type.Borracha)
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
                    if(type == Type.Metal || type == Type.Borracha)
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
                    block.cascateDeletion();
                }
            }
        }

        Destroy(gameObject);
    }
}