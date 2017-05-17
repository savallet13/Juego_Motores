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
            //You has die
            slider_life.value = 0f;

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
    public void updateFood(float food)
    {
        slider_food.value += food;
    }
    public void UpdateScore(Text score)
    {
        if (score.text.Equals("0"))
        {
            score.text += 1;
        }
        else
        {
            score.text += 1+ score.text;
        }
        if (score.text.Equals("6"))
            Application.Quit();
    }
}
