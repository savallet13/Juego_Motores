using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fin : MonoBehaviour
{

    public Texture2D Background;

    



    void Update()
    {
    }   

    void OnGUI()
    {
        SGUI.DrawTexture(0, 0, 100, 100, Background);
        
        
        
    }
}
