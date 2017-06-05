using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Explorar : MonoBehaviour
{
    public Texture2D BackT;
    public Texture2D Instrucciones;
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
        SGUI.DrawTexture(60, 60, 40, 40, Instrucciones);
        float size = 6;
        float sizeb = 10;
        Back = new SGUI.SelfButton(90, 5, size, sizeb, BackT, 1.1f, 1.1f);
        Back.Draw();

    }
}
