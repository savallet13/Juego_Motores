using UnityEngine;

public class MovePlayer : MonoBehaviour {

    //Publics
    public float speed = 6f;
    //Privates
    Vector3 movement;
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

        // Move the player around the scene.
        Move(h, v);
        // Animate the player.
        Animating(h, v);
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
    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = v != 0f;

        // Tell the animator whether or not the player is walking.
        animacion.SetBool("isWalking", walking);
    }



}
