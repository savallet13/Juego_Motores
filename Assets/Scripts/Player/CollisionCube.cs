using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCube : MonoBehaviour {

    private Animator animacion;
    private Rigidbody rigidBodyPlayer;
   

    // Use this for initialization
    void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            animacion = other.gameObject.GetComponent<Animator>();
            

        }
    }
    void OnCollisionStay(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            Animating();

        }
    }
    void OnCollisionExit(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            EndAnimation();

        }
    }
    void EndAnimation()
    {
        animacion.SetBool("isPush", false);
    }
    void Animating()
    {
        animacion.SetBool("isPush",true);
    }
}
