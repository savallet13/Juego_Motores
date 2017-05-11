using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Texture2D Background;

    public Texture2D StartT;

    public Texture2D ExploreT;

    public Texture2D OptionsT;


    SGUI.SelfButton Start;
    SGUI.SelfButton Explore;
    SGUI.SelfButton Options;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (SGUI.PixelInPercentages(Input.mousePosition, Start.Percentages))
            {
                SceneManager.LoadScene("Game");


            }

            if (SGUI.PixelInPercentages(Input.mousePosition, Explore.Percentages))
            {
                // Si pulsamos credits

            }

            if (SGUI.PixelInPercentages(Input.mousePosition, Options.Percentages))
            {
                // Si pulsamos credits

            }
        }
    }

    void OnGUI()
    {
        SGUI.DrawTexture(0, 0, 100, 100, Background);
        
        float size = 15;
        float sizeb = 13;
        Start = new SGUI.SelfButton(10, 70, size, size, StartT, 1.1f, 1.1f);
        Explore = new SGUI.SelfButton(40, 72, size, sizeb, ExploreT, 1.1f, 1.1f);
        Options = new SGUI.SelfButton(70, 70, size, size, OptionsT, 1.1f, 1.1f);
        


        Start.Draw();
        Options.Draw();
        Explore.Draw();
    }
}
