using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public Texture2D Background;

    public Texture2D BackT;


    SGUI.SelfButton Back;
    



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (SGUI.PixelInPercentages(Input.mousePosition, Back.Percentages))
            {
                SceneManager.LoadScene("Menu");


            }
            
        }
    }

    void OnGUI()
    {
        SGUI.DrawTexture(0, 0, 100, 100, Background);
        
        float size = 7;
        Back = new SGUI.SelfButton(50, 70, size, size, BackT, 1.0f, 1.1f);
        
        


        Back.Draw();
        
    }
}
