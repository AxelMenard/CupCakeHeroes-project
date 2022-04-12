using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreen : MonoBehaviour {

    public Animator animator;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadScene()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            SceneManager.LoadScene(2);
        }
        else 
        {
            SceneManager.LoadScene(1);
        }
       
    }

    public void Quit()
    {
        Application.Quit();
    }
}
