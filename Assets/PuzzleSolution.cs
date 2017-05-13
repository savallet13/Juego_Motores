using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolution : MonoBehaviour {


    int cubos = 0; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if ( cubos == 3)
        {

            Debug.Log("Puzzle correcto");

        }
		
	}
    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "cubo 3")
        {
            cubos++;
            Debug.Log("cubos correctos: " + cubos);


        }
    }

    void OnCollisionExit(Collision other)
    {

        if (other.gameObject.tag == "cubo 3")
        {
            cubos--;
            Debug.Log("cubos correctos: " + cubos);

        }

    }
    
}
