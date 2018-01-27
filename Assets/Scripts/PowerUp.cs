using UnityEngine;
using System.Collections;



class PowerUp : MonoBehaviour
{
    public enum Type{Fire,Rock, Acid}
    
    private Type type;

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

    void onCollisionEnter()
    {

    }

    void onCollisionStay()
    {

    }


}