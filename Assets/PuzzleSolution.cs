using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolution : MonoBehaviour {


    public int cubos = 0;

    public GameObject branch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
		
	}
    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "cubo 3")
        {
            cubos++;
            Debug.Log("cubos correctos: " + cubos);

            if (cubos == 3)
            {

                Debug.Log("Puzzle correcto");
                Spawn();
            }


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

    public void Spawn()
    {
        Instantiate(branch, new Vector3(455, 55, 898), Quaternion.identity);

    }

}
