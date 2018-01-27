using UnityEngine;
using System.Collections;


public class PowerUpController : MonoBehaviour
{


	public float waitButtonHoldTime = 0.5f;
    private float holdTimer = 0f;
     private string playerString;
    private string keyboardString;
    private string joystickString;
    public Transform activePowerUp;
    public Transform SpawnPoint;
    public bool aiming = false;
    public PowerUpInventory storedPowerUps;
    public int powerSelection = 0 ;
    public float verticalSpeed= 10f;
    public GameObject[] powerUpPrefabs;
    public bool useJoycon;
    public float controllerDeadZone = 0.1f;
    public float downSpeedUp = 2f;
    public float snapPosition = 1f;

    public float leftContraint;
    public float rightContraint;

    void Start()
    {

    }
    
    void Update()
    {
        if(activePowerUp != null)
        {
            if(aiming)
            {

                if (Input.GetButtonDown(playerString + "Horizontal"))
                {
                    movePower(Input.GetAxisRaw(playerString + "Horizontal"));
                    holdTimer = waitButtonHoldTime/16;
                }
                else if (Mathf.Abs(Input.GetAxisRaw(playerString + "_joy_Horizontal")) > controllerDeadZone && holdTimer <= 0)
                {
                    movePower(Input.GetAxis(playerString + "_joy_Horizontal"));
                    holdTimer = waitButtonHoldTime/16;
                }
                else if(Mathf.Abs(Input.GetAxisRaw(playerString + "_joy_Horizontal")) <= controllerDeadZone)
                {
                    holdTimer = 0;
                }


                //teste de tiro
                if(Input.GetButtonDown(playerString + "Fire"))
                {
                    aiming = false;
                }
            }
            else
            {
                if(!GetComponent<PowerUp>().isTouching())
                {
                    activePowerUp.position += new Vector3(0, -verticalSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            //seleção
            if(Input.GetButtonDown(playerString + "item1"))
            {
                powerSelection = 0;
            }
            else if(Input.GetButtonDown(playerString + "item2"))
            {
                powerSelection = 1;
            }
            else if(Input.GetButtonDown(playerString + "item3"))
            {
                powerSelection = 2;
            }
            else if(Mathf.Abs(Input.GetAxisRaw(playerString + "_joy_item")) > controllerDeadZone && holdTimer <= 0 )
            {
                scrollPower(Input.GetAxis(playerString + "_joy_item"));
                holdTimer = waitButtonHoldTime;
            }
            else if(Mathf.Abs(Input.GetAxisRaw(playerString + "_joy_item")) <= controllerDeadZone)
            {
                holdTimer = 0;
            }

            //selecionar
            if(Input.GetButtonDown(playerString + "Activate")) 
            {
                activateSelection();
            } 
        }
        holdTimer -= Time.deltaTime;
    }

    void scrollPower(float direction)
    {
        if(direction >= 0)
        {
            ++powerSelection;
            if(powerSelection >= 3)
            {
                powerSelection = 0;
            }
        }
        else
        {
            --powerSelection;
            if(powerSelection < 0)
            {
                powerSelection = 2;
            }
        }
        
    }

    void activateSelection()
    {
        activePowerUp = storedPowerUps.popPowerUp(powerSelection).transform;
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

    /* 
    private void checkController() {
        if(Input.GetButtonDown(keyboardString + "Horizontal"))
        {
            useJoycon = false;
            playerString = keyboardString;
        }
        else if (Input.GetAxisRaw(joystickString + "Horizontal") != 0)
        {
            useJoycon = true;
            playerString = joystickString;
        }
    }
    */
    /// <summary>
    /// Move bloco de acordo com o grid
    /// </summary>
    /// <param name="way">Eixo de movimentos</param>
	private void movePower(float axis) {
        int clamped = (axis > 0) ? 1 : -1;
        
        if(activePowerUp.position.x > rightContraint && clamped > 0)
        {
            return;
        }
        if(activePowerUp.position.x < leftContraint && clamped < 0 )
        {
            return;
        }
        Vector3 direction = new Vector3(clamped * snapPosition, 0);
        activePowerUp.position += direction;
    }


}