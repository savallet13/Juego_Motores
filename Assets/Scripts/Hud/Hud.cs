using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

    //Publics
    public Slider slider_life;
    public Slider slider_food;
    public Text count_pieces;
    //Private


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        setFood();
        setLife();
        
    }
    void setLife()
    {
        if(slider_life.value <= 0)
        {
            //You has die!!

        }
        else
        {
            slider_life.value -= 0.01f;
        }

        
    }
    void setFood()
    {
        if (slider_food.value  <= 0)
        {
            //You has angry, need eat!!
            slider_food.value = 0;
            slider_life.value -= 0.05f;

        }
        else
        {
            slider_food.value -= 0.25f;
        }
        
    }
}
