using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{

    public Texture2D Background;

    public Texture2D OnT;

    public Texture2D OffT;

    public Texture2D BackT;


    SGUI.SelfButton On;
    SGUI.SelfButton Off;
    SGUI.SelfButton Back;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (AudioListener.volume == 0 &&  SGUI.PixelInPercentages(Input.mousePosition, On.Percentages))
            {
                AudioListener.volume = 100;


            }

            if (AudioListener.volume != 0 && SGUI.PixelInPercentages(Input.mousePosition, Off.Percentages))
            {
                AudioListener.volume = 0;

            }

            if (SGUI.PixelInPercentages(Input.mousePosition, Back.Percentages))
            {
                SceneManager.LoadScene("Menu");

            }
        }
    }

    void OnGUI()
    {
        SGUI.DrawTexture(0, 0, 100, 100, Background);
        
        float size = 15;
        float sizeb = 10;

        float sized = 6;
        float sizee = 10;

        On = new SGUI.SelfButton(42, 50, size, sizeb, OnT, 1.1f, 1.1f);
        Off = new SGUI.SelfButton(42, 65, size, sizeb, OffT, 1.1f, 1.1f);
        Back = new SGUI.SelfButton(90, 80, sized, sizee, BackT, 1.1f, 1.1f);



        On.Draw();
        Off.Draw();
        Back.Draw();
    }
}
