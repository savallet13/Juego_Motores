﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour {

    public static MovePlayer me;
    //Publics
    public AudioClip punch;
    public AudioClip deathpig;
    public AudioClip comer;
    public AudioClip coger;
    public float speed = 6f;
    public float m_TurnSpeed = 180f;
    public Text cont;
    public Slider slider_food;
    public Hud hud;
    public SpawnFood spf;
    public SpawnEnemy spe;
    Slider Life_Enemie;
    public bool active = true;

    //Privates
    private float m_TurnInputValue;
    private float m_MovementInputValue;
    Vector3 movement;
    Quaternion angle_rotation;
    Animator animacion;
    Rigidbody rigidBodyPlayer;
    EnemyMovement boea;
    private AudioSource source;

    int floorMask;
    float attack = 2.5f;
    
    float jumpForce = 100;
    float time = 0;
    float distoground;
    float jumpCD = 0;
    float jumpseconds= 0;
    float timer = 0;
    float seconds;

    bool canDouble = false;
    bool grounded = true;
    

    float camRayLenght = 100f;

    string m_MovementAxisName;
    string m_TurnAxisName;
    //
    float run ;
    float jump;
    float hit ;
    float take_Object ;

    // Use this for initialization
    void Start () {
        me = this;
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal";

    }
    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        rigidBodyPlayer.isKinematic = false;
        // Also reset the input values.
        m_TurnInputValue = 0f;
    }
    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        rigidBodyPlayer.isKinematic = true;
    }

    // Update is called once per frame
    void Update () {
        // Store the value of both input axes.
        if (!active)
        {
            return;
        }
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }

    //wdqwdqdqw
    void Awake()
    {
        source = GetComponent<AudioSource>();
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
        // Set up references.
        animacion = GetComponent<Animator>();
        rigidBodyPlayer = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (!active)
        {
            return;
        }
        // Store the input axes.
        run = Input.GetAxisRaw("Run");
        jump = Input.GetAxisRaw("Jump");
        hit = Input.GetAxisRaw("Hit");
        take_Object = Input.GetAxisRaw("TakeObject");
        // Move the player around the scene.
        Move();
        Turn();
        Jump();
        // Animate the player.
        Animating(m_TurnInputValue, m_MovementInputValue, run);
        Animating2(m_TurnInputValue, m_MovementInputValue, run);
        Animatiion_Jump(jump);
        Animatiion_WalkJump(m_MovementInputValue,jump);
        Animatiion_Hit(hit);
        Animation_TakeObject(take_Object);
    }
 
    void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * speed * Time.deltaTime;
        // Apply this movement to the rigidbody's position.
        rigidBodyPlayer.MovePosition(rigidBodyPlayer.position + movement);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (grounded && jumpCD == 0)
            {
                rigidBodyPlayer.velocity = new Vector3(0, 0, 0);
                rigidBodyPlayer.MovePosition(rigidBodyPlayer.position +(new Vector3(0, jumpForce, 0)*Time.deltaTime));
                
                canDouble = true;
                grounded = false;
            }
            else
            {
                if (canDouble)
                {
                    canDouble = false;
                    grounded = true;
                    rigidBodyPlayer.velocity = new Vector3(0, 0, 0);
                    rigidBodyPlayer.AddForce(new Vector3(0, 350, 0));
                    jumpCD = 1;
                }
            }          
            }

    if ( jumpCD == 1) {
                timer += Time.deltaTime;
                seconds = timer % 60;
            }
    if (seconds > 2) {
                jumpCD = 0;
                timer = 0;
                seconds = 0;  
            }    
    }  
    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        // Apply this rotation to the rigidbody's rotation.
        rigidBodyPlayer.MoveRotation(rigidBodyPlayer.rotation * turnRotation);
    }
    void Animatiion_Jump(float jump)
    {
        bool j = jump != 0f;
        animacion.SetBool("isJump",j);
    }
    void Animatiion_WalkJump(float jump, float walk)
    {
        bool jw = jump!=0f && walk!=0;
        animacion.SetBool("isWalkJump", jw);
    }
    void Animatiion_Hit(float hit)
    {
        bool golpe = hit != 0f;
        animacion.SetBool("isHit", golpe);
    }
    void Animation_TakeObject(float take_object)
    {
        bool take = take_object != 0f;
        animacion.SetBool("isTakeObject", take);
    }
    void Animating(float h, float v,float r)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = v!=0f && r==0;
        animacion.SetBool("isWalking", walking);
    }
    void Animating2(float h, float v, float r)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool run = r == 1;

        if (run)
            speed=25;
        else
            speed = 10f;
        animacion.SetBool("isRunning", run);
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Canoa")
        {
            Destroy(other.gameObject);
            source.PlayOneShot(coger);
            hud.AddScore(1);
        }
        if (other.tag.Equals("Respawn"))
        {
            hud.updateFood(200f);
            int num_sp = int.Parse(other.name.Substring(1));
            spf.StartCoroutine(spf.prova(num_sp-1));
            source.PlayOneShot(comer);
            Destroy(other.gameObject);
            
        }
        if (other.tag.Equals("Barco"))
        {
            SceneManager.LoadScene("End");
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Cerdo") && hit==1)
        {
            source.PlayOneShot(punch);
            boea = GameObject.FindGameObjectWithTag("Cerdo").GetComponent<EnemyMovement>();
            boea.setSlider(attack);
            
            if (boea.getValueSlider()<=0)
            {
                Destroy(other.gameObject);
                source.PlayOneShot(deathpig);
                spe.NumberEnemies--;
            }
        }
    }
    
}
