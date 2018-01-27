using UnityEngine;
using System.Collections;


public class PowerUpController : MonoBehaviour
{

    public GameObject activePowerUp;
    public bool aiming = false;
    public PowerUpInventory storedPowerUps;
    public int powerSelection = 0 ;
    public float verticalSpeed= 10f;
    public GameObject[] powerUpPrefabs;

    
    void Update()
    {
        if(activePowerUp != null)
        {
            if(aiming)
            {
                //TODO: input e movimentação de mira do powerup
            }
            else
            {
                
            }
        }
        else
        {
            //TODO: colocar o teste de mudar o powerup a ser selecionado
            if(false)
            {
                nextPower();
            }
            //TODO: colocar o teste de atirar
            if(false) 
            {
                activateSelection();
            }

            
        }
    }

    void nextPower()
    {
        ++powerSelection;
        if(powerSelection >= storedPowerUps.powerUpCount())
        {
            powerSelection -= storedPowerUps.powerUpCount();
        }
    }

    void activateSelection()
    {
        activePowerUp = storedPowerUps.popPowerUp(powerSelection);
        if(activePowerUp != null)
        {
            powerSelection = 0;
            aiming = true;
        }
        
    }
    void newPowerUp()
    {
        int power = Random.Range(0, powerUpPrefabs.Length - 1);
        storedPowerUps.pushPowerUp(Instantiate(powerUpPrefabs[power]));
    }


}