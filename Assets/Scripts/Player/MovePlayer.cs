using UnityEngine;

public class MovePlayer : MonoBehaviour {

    //Publics
    public float speed = 6f;
    public float m_TurnSpeed = 180f;
    private float m_TurnInputValue;
    private float m_MovementInputValue;
    //Privates
    Vector3 movement;
    Quaternion angle_rotation;
    Animator animacion;
    Rigidbody rigidBodyPlayer;
    int floorMask;

    float camRayLenght = 100f;

    string m_MovementAxisName;
    string m_TurnAxisName;

    // Use this for initialization
    void Start () {
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
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }

    //wdqwdqdqw
    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
        // Set up references.
        animacion = GetComponent<Animator>();
        rigidBodyPlayer = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {
        // Store the input axes.
        float run = Input.GetAxisRaw("Jump");
        // Move the player around the scene.
        Move();
        Turn();
        // Animate the player.
        Animating(m_TurnInputValue, m_MovementInputValue, run);
        Animating2(m_TurnInputValue, m_MovementInputValue, run);
    }
  
    void RotationCharacter(float rotation)
    {

    }
    void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * speed * Time.deltaTime;
        // Apply this movement to the rigidbody's position.
        rigidBodyPlayer.MovePosition(rigidBodyPlayer.position + movement);
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
            speed=15;
        else
            speed = 5.5f;
        animacion.SetBool("isRunning", run);
        
    }





}
