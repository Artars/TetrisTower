using UnityEngine;
using System.Collections;

/*


 */
public class Block : MonoBehaviour{

    public enum Type{Wood, Glass, Metal, Rock};
    public enum Shape{I,J,L,O,S,Z,T};

    private bool deleting = false;

    [HideInInspector]
    public bool controlable;

    private bool gettingDeleted;
    public Type type;
    public Shape shape;
    public Rigidbody2D rb;
    private BlockController controller;
    public int player = -1;
    public AudioClip contactSound;
    public AudioClip destroySound;
    public float silenceTime = 0.5f;

    private AudioSource audioSource; 
    private float deletionTimer = 0f;
    private static readonly float deletionDuration = 1f;
    private static float killY = -15f;
    private Transform myTransform;
    private static float standardVelocitySound = 2f;
    private float silenceTimer;

    void Start()
    {
        myTransform = transform;
        controlable = true;
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
            //rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        //rb.isKinematic = true;
        gettingDeleted = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(deleting)
        {
            deletionTimer += Time.deltaTime;
            if(deletionTimer > deletionDuration )
            {
                Destroy(gameObject);
            }
        }

        
        if (myTransform.position.y < killY) {
            if (controller != null)
                controller.stopHoldingBlock();
            Destroy(gameObject);
        }
        silenceTimer -= Time.deltaTime;
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
        collideSound(other.gameObject);
        //print("Colide: " + other.gameObject.name);
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

    private void collideSound(GameObject other) {
        if (silenceTimer > 0)
            return;
        audioSource.clip = contactSound;

        float velocity = rb.velocity.magnitude;
        float volume = (velocity / standardVelocitySound) - 0.5f;
        if (volume < 0.1f)
            volume = 0.1f;

        audioSource.volume = volume;
        audioSource.Play();

        if (other != null)
        {
            Transform parent = other.transform.parent;
            if (parent != null && parent.gameObject.tag.Equals("Block"))
            {
                Block blockScript = parent.GetComponent<Block>();
                blockScript.playCollideSound(volume / 2);
            }
        }
        silenceTimer = silenceTime;
    }

    public void playCollideSound(float volume) {
        if (silenceTimer <= 0)
        {
            audioSource.clip = contactSound;

            audioSource.volume = volume;
            audioSource.Play();
            silenceTimer = silenceTime;
        }
        
    }

    public void playDestructionSound() {
        audioSource.clip = destroySound;

        audioSource.volume = 1;
        audioSource.Play();
    }
}