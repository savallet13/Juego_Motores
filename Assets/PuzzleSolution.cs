using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolution : MonoBehaviour {

    bool result = false;
    int cubos = 0;
    public GameObject piece;
    public AudioClip puzzlecorrecto;
    private AudioSource source;

    // Use this for initialization
    void Start () {
		
	}

    void Awake()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update () {

        if ( cubos == 3)
        {

            Debug.Log("Puzzle correcto");
            source.PlayOneShot(puzzlecorrecto);

        }
		
	}
    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "cubo 3")
        {
            cubos++;
            Debug.Log("cubos correctos: " + cubos);

            if(cubos == 3 && result == false)
            {

                Instantiate(piece, new Vector3(465, 52, 882), Quaternion.identity);
                result = false;
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
    
}
