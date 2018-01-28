using UnityEngine;
using System.Collections;



public class PowerUp : MonoBehaviour
{
    public enum Type{Fire,Rock, Acid}
    
    private Type type;

    
    private Rigidbody2D rigid;

    private float timeElapsed;

    //time before the powerup is destroyed
    private readonly float timeBeforeDestroy = 0.5f; 

    private bool touching = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
    }

    public bool isTouching()
    {
        return touching;
    }


    void setType(Type nextType)
    {
        type = nextType;
        switch(type)
        {
            case Type.Fire:
                gameObject.tag = "Power_Fire";
                break;
            case Type.Rock:
                gameObject.tag = "Power_Rock";
                break;
            case Type.Acid:
                gameObject.tag = "Power_acid";
                break;
        }
    }

    public Type getType()
    {
        return type;
    }

    void onTriggerEnter2D(Collider col)
    {
        Block block = col.gameObject.GetComponent<Block>();

        if(block != null)
        {
            if(!block.isControlable())
            {
                timeElapsed = 0;
                touching = true;
            }
        }
    }

    void onTriggerStay2D(Collider2D col)
    {
        Block block = col.gameObject.GetComponent<Block>();

        if(block != null)
        {
            if(!block.isControlable())
            {
                timeElapsed += Time.deltaTime;
                if(timeElapsed > timeBeforeDestroy)
                {
                    Destroy(gameObject);
                }
            }
        }
    }


}