using UnityEngine;
using System.Collections;



public class Player : MonoBehaviour
{
    public int id = 0;
    public string nome;
    public BlockController blockCtr;
    public PowerUpController powerUpCtr;

    public float maxLeft;
    public float maxRight;

    public float getCenter()
    {
        return (maxLeft+maxRight)/2;
    } 


}