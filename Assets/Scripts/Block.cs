using UnityEngine;
using System.Collections;

/*


 */
public class Block : MonoBehaviour{

    enum Type{Wood, Glass, Metal};
    enum Shape{T,Z,_Z,I,L,_L};

    public bool controlable;
    public Type type;
    public Shape shape;
    public Rigidbody2D rb;

    void Start()
    {
        controlable = true;
        rb = ganeObject.addComponent(typeof(Rigidbody2D));
        rb.isKinematic = true;
    }

    /*
        Tocar em outro bloco
     */
    void OnCollisionEnter(Collision col)
    {
        if(controlable)
        {
            if(col.gameObject.tag == "Block" ||  || col.gameObject.tag == "Ground")
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
                    if(type == Wood || type == Borracha)
                    {
                        cascateDeletion();
                    }
                    break;
                case "power_rock": 
                    if(type == Glass || type == Wood)
                    {
                        cascateDeletion();
                    }
                    break;
                case "power_acid":
                    if(type == Metal || type == Borracha)
                    {
                        cascateDeletion();
                    }
                    break;

            }
        }
    }

    private void cascateDeletion()
    {
        // do the animation
        Block[] blocks = Object.FindObjectsOfType(Block) as Block[];

        foreach (block item in blocks)
        {
            if(block == this)
            {
                continue;
            }

            if(block.isTouching(this))
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