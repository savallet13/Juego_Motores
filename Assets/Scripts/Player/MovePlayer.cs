using UnityEngine;

public class MovePlayer : MonoBehaviour {

    //Publics
    public float speed = 6f;
    //Privates
    Vector3 movement;
    Quaternion angle_rotation;
    Animator animacion;
    Rigidbody rigidBodyPlayer;
    int floorMask;

    float camRayLenght = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float run = Input.GetAxisRaw("Jump");

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0, 90, 0);
        }

        //CapturKeyPressed();
        // Move the player around the scene.
        Move(h, v);
        // Animate the player.
        Animating(h, v,run);
        Animating2(h, v, run);
    }
    float CapturKeyPressed()
    {
        bool key_a = Input.GetKey("a");
        bool key_d = Input.GetKey("d");
        bool key_w = Input.GetKey("w");
        bool key_s = Input.GetKey("s");

        if (key_a)
            transform.Rotate(0, 180, 0);
        if (key_d)
            transform.Rotate(0, 90, 0);
        if (key_w)
            transform.Rotate(0, -180, 0);
        if (key_s)
            transform.Rotate(0, 180, 0);


        return 0f;

    }
    void RotationCharacter(float rotation)
    {

    }
    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;
        
        // Move the player to it's current position plus the movement.
        rigidBodyPlayer.MovePosition(transform.position + movement);
        



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
