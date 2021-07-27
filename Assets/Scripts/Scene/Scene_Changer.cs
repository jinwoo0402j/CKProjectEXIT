using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Changer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Exit();
    }

    public void Scene_Change()
    {
        SceneManager.LoadScene("TestPlayerMovement");
    }
    public void Scene_ReStart()
    {
        SceneManager.LoadScene("Lobby");
    }


    public void Exit_Botton()
    {
        Application.Quit();
    }

    public void Exit()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
