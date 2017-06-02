using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public Texture2D Background2;

    public Texture2D FlechaT;
    

    SGUI.SelfButton Flecha;
    



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (SGUI.PixelInPercentages(Input.mousePosition, Flecha.Percentages))
            {
                SceneManager.LoadScene("Game");


            }
            
        }
    }

    void OnGUI()
    {
        SGUI.DrawTexture(0, 0, 100, 100, Background2);
        
        float size = 15;
        Flecha = new SGUI.SelfButton(80, 80, size, size, FlechaT, 1.2f, 1.1f);
        
        


        Flecha.Draw();
        
    }
}
